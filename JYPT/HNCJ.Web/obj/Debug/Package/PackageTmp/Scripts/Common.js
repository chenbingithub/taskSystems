var Common = {
    datagrid: {},
    dialog:{},
    urls: {
        urladd: "",
        urledit: "",
        urldelete:""
    },
    //初始化
    Init: function (datagrid, dialog, urls) {
        this.datagrid = datagrid;
        this.dialog = dialog;
        this.urls = urls;
    },
    
    //重新加载表格
    ReloadDatagrid: function () {
        this.datagrid.datagrid('load');
    },
    //重新加载表格
    doSearch: function () {
        this.datagrid.datagrid('load', {
            KeyString: $("#KeyString").val()
        });
    },
    //显示是否有效
    IsChecked: function (value, row) {
        if (value == 1) {
            return '<a href="#" onclick=\'Common.SetStatus(' + row.ID + ',' + value + ')\'><span class="icon icon-chk_checked">&nbsp;</span></a>';
        } else {
            return '<a href="#" onclick=\'Common.SetStatus(' + row.ID + ',' + value + ')\'><span class="icon icon-chk_unchecked">&nbsp;</span></a>';
        }

    },
    SetStatus: function (roleid, state) {
        var $this = this;
        $.post("/Roleinfo/SetStatus", { roleid: roleid, state: state }, function(data) {
            if (data.status == 1) {

                $this.ReloadDatagrid();
            } else {
                $.messager.show({
                    title: '提示',
                    msg: data.msg,
                    showType: 'slide',
                    timeout: 2000,
                    style: {
                        right: '',
                        top: document.body.scrollTop + document.documentElement.scrollTop,
                        bottom: ''
                    }
                });
            }
        });
    },

    //公用按钮
    ButtonCommon: function () {
        var $this = this;
        $("#ff").form('submit', {
            onSubmit: function () {
                var isValid = $(this).form('enableValidation').form('validate');

                if (!isValid) {
                    $.messager.progress('close');
                }
                return isValid;
            },
            success: function (data) {
                var data = eval('(' + data + ')'); // change the JSON string to javascript object
                if (data.status == 1) {
                    $this.dialog.dialog('close');
                    $.messager.show({
                        title: '提示',
                        msg: data.msg,
                        showType: 'slide',
                        timeout: 2000,
                    });
                    $this.ReloadDatagrid();
                } else {
                    $.messager.show({
                        title: '提示',
                        msg: data.msg,
                        showType: 'slide',
                        timeout: 2000,
                        style: {
                            right: '',
                            top: document.body.scrollTop + document.documentElement.scrollTop,
                            bottom: ''
                        }
                    });
                }
            }
        });
    },
    //点击新增
    addClick: function () {
        var $this = this;
        var url = $this.urls.urladd;
        $this.dialog.dialog({
            title: '添加',
            width: 600,
            height: 300,
            href: url,
            closed: false,
            cache: false,
            maximizable: true,
            modal: true,
            buttons: [{
                text: '确定',
                handler: function () {
                    $this.ButtonCommon();
                }
            }, {
                text: '关闭',
                handler: function () {
                    $this.dialog.dialog('close');

                }
            }]
        });
    },
    //编辑方法
    editClick: function () {
        var $this = this;
        var rows = $this.datagrid.datagrid('getSelections');
        if (rows.length != 1) {
            $.messager.show({
                title: '提示',
                msg: "请选择一行",
                showType: 'slide',
                timeout: 2000,
            });
            return;
        }
        var row = $this.datagrid.datagrid('getSelected');
        var url = $this.urls.urledit+"/" + row.ID;
        $this.dialog.dialog({
            title: '编辑',
            width: 600,
            height: 300,
            href: url,
            closed: false,
            cache: false,
            maximizable: true,
            modal: true,
            buttons: [{
                text: '确定',
                handler: function () {
                    $this.ButtonCommon();
                }
            }, {
                text: '关闭',
                handler: function () {
                    $this.dialog.dialog('close');

                }
            }]
        });
    },
    //多选删除
    deleteClick: function () {
        var $this = this;
        var rows = $this.datagrid.datagrid('getSelections');
        if (rows.length == 0) {
            $.messager.show({
                title: '提示',
                msg: "请至少选择一行",
                showType: 'slide',
                timeout: 2000
            });
            return;
        }
        var url = $this.urls.urldelete;
        var ids = "";
        if (rows.length > 0) {
            for (var key in rows) {
                ids = ids + rows[key].ID + ",";
            }
            ids = ids.substr(0, ids.length - 1);
            $.messager.confirm('Confirm', '你确定要删除吗?', function (r) {
                if (r) {
                    $.post(url, { ids: ids }, function (data) {
                        if (data.status == 1) {
                            $.messager.show({
                                title: '提示',
                                msg: data.msg,
                                showType: 'slide',
                                timeout: 2000,
                            });
                            $this.ReloadDatagrid();
                        } else {
                            $.messager.show({	// show error message
                                title: 'Error',
                                msg: data.msg
                            });
                        }
                    }, 'json');
                }
            });
        }
        return;
    },
    TreePermission: function () {
        var rows = $('#dg').datagrid('getSelections');
        if (rows.length != 1) return;
        var row = $('#dg').datagrid('getSelected');
        $.fn.zTree.init($("#roletreePermission"), {
            data: {
                key: {
                    name: "ActionName"
                },
                simpleData: {
                    enable: true,
                    idKey: "ID",
                    pIdKey: "ParentId",
                    //rootPId: -1
                }
            },
            check: {
                enable: true,
            },

            async: {
                enable: true,
                url: "/Roleinfo/GetPermissionList",
                otherParam: { roleid: row.ID }
            },

        });
    },
    OpenPermissionView: function () {
        var rows = $('#dg').datagrid('getSelections');
        if (rows.length != 1) return;
        var url = '/roleinfo/setPermission';
        $('#roleinfodialog').dialog({
            title: '分配操作权限',
            width: 400,
            height: 300,
            closed: false,
            cache: false,
            href: url,
            modal: true,
            buttons: [{
                text: '确定',
                handler: function () {
                    var treeObj = $.fn.zTree.getZTreeObj("roletreePermission");
                    var nodes = treeObj.getCheckedNodes(true);
                    var actionids = "";
                    for (var i in nodes) {
                        actionids += nodes[i].ID + ",";
                    }
                    actionids = actionids.substr(0, actionids.length - 1);
                    var row = $('#dg').datagrid('getSelected');
                    $.post("/roleinfo/ProcessSetPermission", { roleid: row.ID, actionids: actionids }, function (data) {
                        if (data.status == 1) {
                            $('#roleinfodialog').dialog('close');
                        } else {
                            $.messager.show({	// show error message
                                title: 'Error',
                                msg: data.msg
                            });
                        }
                    });

                }
            }, {
                text: '关闭',
                handler: function () {
                    $('#roleinfodialog').dialog('close');

                }
            }]
        });
    },
};
/**     
 * 对Date的扩展，将 Date 转化为指定格式的String     
 * 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符     
 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)     
 * eg:     
 * (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423     
 * (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04     
 * (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04     
 * (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04     
 * (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18     

使用：(eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d h:m:s.S");

 */
String.prototype.formatter = function (value) {
    return (eval(this.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern(value);
}
Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份        
        "d+": this.getDate(), //日        
        "h+": this.getHours() % 24 == 0 ? 24 : this.getHours() % 24, //小时        
        "H+": this.getHours(), //小时        
        "m+": this.getMinutes(), //分        
        "s+": this.getSeconds(), //秒        
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度        
        "S": this.getMilliseconds() //毫秒        
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}