using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNCJ.Model
{
    public class Enum
    {
        /// <summary>
        /// 流程过程枚举类型
        /// </summary>
        public enum WFStepEnum
        {
            /// <summary>
            /// 同意
            /// </summary>
            Proecessed = 1,
            /// <summary>
            /// 不同意
            /// </summary>
            UnProecess = 2
        }

        /// <summary>
        /// 流程处理枚举类型
        /// </summary>
        public enum WFInstanceEnum
        {
            /// <summary>
            /// 正在处理
            /// </summary>
            Proecessing = 1,
            /// <summary>
            /// 待处理
            /// </summary>
            Proecessed = 2
        }
        /// <summary>
        /// 交易状态枚举类型
        /// </summary>
        public enum OrderInfoEnum
        { 
            /// <summary>
            /// 待付款
            /// </summary>
            WaitPayment=1,
            /// <summary>
            /// 待发货
            /// </summary>
            WaitSend= 2,
            /// <summary>
            /// 待收货
            /// </summary>
            WaitDelivery = 3,
            /// <summary>
            /// 已确认收货
            /// </summary>
            ReceivedGoods=4,
            
            /// <summary>
            /// 交易成功
            /// </summary>
            Success=5,
            /// <summary>
            /// 待评价
            /// </summary>
            WaitEvaluate = 6,
            /// <summary>
            /// 退款中
            /// </summary>
            Refunding=7,
            /// <summary>
            /// 退款成功
            /// </summary>
            RefundSuccess=8,
            /// <summary>
            /// 交易关闭
            /// </summary>
            Error=9

        }

        public enum ShopCardEnum {
            /// <summary>
            /// 待付款
            /// </summary>
            WaitPayment = 1,
            /// <summary>
            /// 已付款
            /// </summary>
            FinishPayment=2
        }

        /// <summary>
        /// 商品状态
        /// </summary>
        public enum GoodsInfoEnum {
            /// <summary>
            /// 上架商品
            /// </summary>
            UpShelves = 1,
            /// <summary>
            /// 下架商品
            /// </summary>
            DownShelves = 2,
            /// <summary>
            /// 库存不足
            /// </summary>
            NoStock=3,
            /// <summary>
            /// 删除该商品
            /// </summary>
            Delete=4
        }
    }
}
