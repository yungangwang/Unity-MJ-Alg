using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour 
{
	CMJ[] t_OtherPlayer = new CMJ[3]{new CMJ(), new CMJ(), new CMJ()};  /*其余三个人*/
	CMJ t_MyPlayer = new CMJ();  /*自己*/
	CMJManage t_MJManage = new CMJManage();  /*麻将管理器*/
	int t_Score = 0;　/*分数*/

	void Start () 
	{
		t_MJManage.InitPai(0);

		t_MyPlayer.CleanUp();  
		for(int i = 0 ; i < 3; i++ )  
		{  
			t_OtherPlayer[i].CleanUp();  
		}  

		Debug.Log("洗牌完成");
		Debug.Log("起牌:========================================================");

		for(int i = 0 ; i < 13 ; i++)  
		{  
			stPAIEx t_Pai = t_MJManage.GetAPai();  
			t_MyPlayer.AddPai(t_Pai.m_NewPai.m_Type, t_Pai.m_NewPai.m_Value);  

			for(int j = 0 ; j < 3; j++)  
			{  
				stPAIEx t_Pai2 = t_MJManage.GetAPai();  
				t_OtherPlayer[j].AddPai(t_Pai2.m_NewPai.m_Type, t_Pai2.m_NewPai.m_Value);  
			}  
		} 

		t_MyPlayer.Init();  
		for(int j = 0 ; j < 3; j++ )  
			t_OtherPlayer[j].Init(); 

		t_MyPlayer.PrintAllPai("我的牌: ");

		//打牌循环;
//		bool t_Finish = false;  
//		bool t_Ting = false;  
//		while(t_Finish == false)  
//		{  
//			t_MyPlayer.PrintAllPai("我的牌: ");
//			stPAIEx t_Pai = t_MJManage.GetAPai(); 
//
//			//刷新我方牌墙  
//			t_MyPlayer.PrintPai(t_Pai.m_NewPai.m_Type, t_Pai.m_NewPai.m_Value);  
//
//			//如果没有听头  
//			if(t_Ting == false)
//			{
//				
//			}
//		}
	}
}
