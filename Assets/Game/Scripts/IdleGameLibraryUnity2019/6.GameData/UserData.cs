using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public class WzData {
    public string date;

    public List<float> investmentDatas = new List<float>();
    
    public List<float> consumptionDatas = new List<float>();

    public float result1 = 0;  //当月收入，投资部分
    public float result2 = 0;  //当月收入，消费部分
    public float result3 = 0;  //投资部分，总计
    public float result4 = 0;  //投资部分，上月
    public float result5 = 0;  //投资部分，盈亏
    public float result6 = 0;  //消费部分，总计
    public float result7 = 0;  //消费部分，上月
    public float result8 = 0;  //消费部分，花销
    public float result9 = 0;  //所有者权益
    public float result10 = 0; //攒了多少
    public float result11 = 0; //当月总收入
    public float result12 = 0; //当月总收入的投资占比
    public float result13 = 0; //公积金新增总额
    public float result14 = 0;
    public float result15 = 0;
    public float result16 = 0;
    public float result17 = 0;
    public float result18 = 0;
    public float result19 = 0;
    public float result20 = 0;
}

[System.Serializable]
public class UserData {
    public List<WzData> myDatas;
    public List<string> investmentDisplays;
    public List<string> consumptionDisplays;
    public List<string> resultDisplays;


    public void SetDefaultData()
    {
        myDatas = new List<WzData>();
        investmentDisplays = new List<string>();
        consumptionDisplays = new List<string>();

        investmentDisplays.Add("余额宝");
        investmentDisplays.Add("蚂蚁金融");
        investmentDisplays.Add("同花顺");
        investmentDisplays.Add("华宝智投");
        investmentDisplays.Add("公积金");
        investmentDisplays.Add("招商余额");
        investmentDisplays.Add("闪电贷");

        consumptionDisplays.Add("微信零钱");
        consumptionDisplays.Add("余利宝");
        consumptionDisplays.Add("房租房贷");
        consumptionDisplays.Add("花呗");
        consumptionDisplays.Add("交通信卡");
        consumptionDisplays.Add("建设信卡");
        consumptionDisplays.Add("中信信卡");
        consumptionDisplays.Add("京东白条");

    }
    
}
