using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DefalutPlugin.Convertor
{
    [ValueConversion(typeof(int), typeof(string))]
    public class BatteryChemistry : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (int.Parse(value.ToString()!))
            {
                case 1:
                    return "其他";
                case 2:
                    return "不详";
                case 3:
                    return "铅酸";
                case 4:
                    return "镍镉";
                case 5:
                    return "镍金属氢化物";
                case 6:
                    return "锂离子";
                case 7:
                    return "锌空气";
                case 8:
                    return "锂聚合物";
                case 0:
                    return "不详";
                default:
                    return "不详";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "其他":
                    return 1;
                case "不详":
                    return 2;
                case "铅酸":
                    return 3;
                case "镍镉":
                    return 4;
                case "镍金属氢化物":
                    return 5;
                case "锂离子":
                    return 6;
                case "锌空气":
                    return 7;
                case "锂聚合物":
                    return 8;
                default:
                    return 0;
            }
        }
    }


    [ValueConversion(typeof(int), typeof(string))]
    public class BatteryStatue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (int.Parse(value.ToString()!))
            {
                case 1:
                    return "电池正在放电";
                case 2:
                    return "正在交流电";
                case 3:
                    return "满电";
                case 4:
                    return "电量低";
                case 5:
                    return "严重电量低";
                case 6:
                    return "正在充电";
                default:
                    return "你的电池寄了";
            }
            return "你的电池寄了";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "电池正在放电":
                    return 1;
                case "正在交流电":
                    return 2;
                case "满电":
                    return 3;
                case "电量低":
                    return 4;
                case "严重电量低":
                    return 5;
                case "正在充电":
                    return 6;
                default:
                    return "你的电池寄了";
            }
            return 0;//灵异事件
        }
    }
    
}
