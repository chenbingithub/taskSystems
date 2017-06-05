//全选取消按钮函数，调用样式如：
function checkAll(chkobj) {
    if ($(chkobj).text() == "全选") {
        $(chkobj).text("取消");
        $(".checkall").prop("checked", true);
    } else {
        $(chkobj).text("全选");
        $(".checkall").prop("checked", false);
    }
}
//执行删除操作
function ExecDelete(sendUrl, checkValue, urlObj) {
    //检查传输的值
    if (!checkValue) {
        dialog({ title: '提示', content: '对不起，请选中您要操作的记录！', okValue: '确定', ok: function () { } }).showModal();
        return false;
    }

    dialog({
        title: '提示',
        content: '删除记录后不可恢复，您确定吗？',
        okValue: '确定',
        ok: function () {
            $.ajax({
                type: "POST",
                url: sendUrl,
                dataType: "json",
                data: {
                    Id: checkValue
                },
                timeout: 20000,
                success: function (data, textStatus) {
                    if (data.status == "ok") {
                        var tipdialog = dialog({ content: data.msg }).show();
                        setTimeout(function () {
                            tipdialog.close().remove();
                            location.reload();

                        }, 2000);
                    } else {
                        dialog({ title: '提示', content: data.msg, okValue: '确定', ok: function () { } }).showModal();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    dialog({ title: '提示', content: '状态：' + textStatus + '；出错提示：' + errorThrown, okValue: '确定', ok: function () { } }).showModal();
                }
            });
        },
        cancelValue: '取消',
        cancel: function () { }
    }).showModal();
}
//单击执行AJAX请求操作
function clickSubmit(sendUrl) {
    $.ajax({
        type: "POST",
        url: sendUrl,
        dataType: "json",
        timeout: 20000,
        success: function (data,textStatus) {
            if (data.status=="ok") {
                var d = dialog({ content: data.msg }).show();
                setTimeout(function () {
                    d.close().remove();
                    location.reload();
                }, 2000);
            } else {
                dialog({ title: '提示', content: data.msg, okValue: '确定', ok: function () { } }).showModal();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            dialog({ title: '提示', content: "状态：" + textStatus + "；出错提示：" + errorThrown, okValue: '确定', ok: function () { } }).showModal();
        }
    });
}