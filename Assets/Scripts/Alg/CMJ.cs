using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://blog.csdn.net/honghaier/article/details/8604955

//  m_Type      m_Value  
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//  
//  0       |   中   1   发2  白                                               
//          |  
//  1       |   东 1 西2  南     北                                    
//          |  
//  2       |   一万  二万  ……  九万  
//          |  
//  3       |   一条  二条  ……  九条                    
//          |  
//  4       |   一饼  二饼  ……  九饼  
//          |  
//  5       |   春       夏       秋       东       竹       兰       梅       菊  
//          |  
//-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//  

// 节点信息;
public struct stPAI  
{  
	public int     m_Type;             //牌类型;  
	public int     m_Value;            //牌字;
};

// 吃牌顺; 
public struct stCHI                      
{  
	public int     m_Type;             //牌类型;
	public int     m_Value1;           //牌字;
	public int     m_Value2;           //牌字;
	public int     m_Value3;           //牌字;
};

//胡牌信息;  
public struct stGoodInfo  
{  
	public char[]    m_GoodName;              //胡牌术语(100); 
	public int       m_GoodValue;             //胡牌番数; 
}; 

public class CMJ
{
	List<int>[]        m_MyPAIVec = new List<int>[6]{new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>()};      //起的种牌型(6);
	List<int>[]        m_ChiPAIVec = new List<int>[6]{new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>()};     //吃的种牌型(6);  
	List<int>[]        m_PengPAIVec = new List<int>[6]{new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>()};    //碰的种牌型(6);  
	List<int>[]        m_GangPAIVec = new List<int>[6]{new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>()};    //杠的种牌型(6); 

	stPAI               m_LastPAI;          //最后起的牌; 
	stGoodInfo          m_GoodInfo;         //胡牌信息;  

	bool                m_9LBD;             //是否听连宝灯牌型;  
	bool                m_13Y;              //是否听十三幺;  
	int                 m_MKNum;            //明刻数;  
	int                 m_AKNum;            //暗刻数;  
	bool                m_4AK;              //是否是听四暗刻; 

	List<stCHI>      m_TempChiPAIVec = new List<stCHI>();    //吃的可选组合;  
	List<stPAI>      m_TempPengPAIVec = new List<stPAI>();   //碰的可选组合;  
	List<stPAI>      m_TempGangPAIVec = new List<stPAI>();   //杠的可选组合;  

	public void Init()  
	{  
		m_9LBD  = false;  
		m_13Y   = false;  
		m_4AK   = false;  
		m_AKNum = 0;  
		m_MKNum = 0;  
	}  

	//加入新牌,并排序;
	public bool AddPai(int p_Type, int p_Value)  
	{  
		int iSize = m_MyPAIVec[p_Type].Count;  
		bool t_Find = false;  

		for(int i = 0; i < iSize; i++)
		{
			if(m_MyPAIVec[p_Type][i] > p_Value)
			{
				m_MyPAIVec[p_Type].Add(p_Value);  
				t_Find = true;  
				break;  
			}
		}

		if(!t_Find)  
		{  
			m_MyPAIVec[p_Type].Add(p_Value);  
		}  

		m_LastPAI.m_Type = p_Type;  
		m_LastPAI.m_Value = p_Value;  

		return true;  
	} 

	//取得对应的牌在牌墙的索引; 
	public int GetPaiIndex(int p_Type, int p_Value)  
	{  
		int count = 0;  
		for(int i = 0 ; i < 6 ; i++ )  
		{  
//			vector<  int >::iterator Iter;  
//			for(Iter = m_MyPAIVec[i].begin();Iter !=m_MyPAIVec[i].end(); Iter++)  
//			{  
//				if(p_Type==i&&(*Iter)==p_Value)  
//				{  
//					return count;  
//				}  
//				count++;  
//			}  
		}  
		return -1;  
	} 

	//打牌  
	public bool DelPai(int PaiIndex)  
	{  
//		int count = 0;  
//		for(UINT i = 0 ; i < 6 ; i++ )  
//		{  
//			vector<  int >::iterator Iter;  
//			for(Iter = m_MyPAIVec[i].begin();Iter !=m_MyPAIVec[i].end(); Iter++)  
//			{  
//				if(count==PaiIndex)  
//				{  
//					m_MyPAIVec[i].erase(Iter);  
//					return true;  
//				}  
//				count++;  
//			}  
//		}  
		return false;  
	}  

	//删除牌  
	public bool DelPai(int p_Type,int p_Value)  
	{  
//		vector<  int >::iterator Iter;  
//		for(Iter = m_MyPAIVec[p_Type].begin();Iter !=m_MyPAIVec[p_Type].end(); Iter++)  
//		{  
//			if((*Iter)==p_Value)  
//			{  
//				m_MyPAIVec[p_Type].erase(Iter);  
//				return true;  
//			}  
//		}  
		return false;  
	}  

	//清空牌  
	public void CleanUp()  
	{  
		for(int i = 0 ; i < 6 ; i++ )  
		{  
			m_MyPAIVec[i].Clear();  
			m_ChiPAIVec[i].Clear();  
			m_PengPAIVec[i].Clear();  
			m_GangPAIVec[i].Clear();  
		}  
	}  

	//对所有的牌进行函数调用  
	public void PrintAllPai(string strMsg)  
	{  
		string msg = strMsg;
		int icount = 0;  

		//箭牌  
		if(m_MyPAIVec[0].Count > 0)  
		{  
			for(int i = 0; i < m_MyPAIVec[0].Count; i++)
			{  
				switch(m_MyPAIVec[0][i])  
				{  
				case 1:  
					msg +="[ 中]";
					break;  
				case 2: 
					msg +="[ 发]";
					break;  
				case 3:  
					msg +="[ 白]";
					break;  

				}  
				icount++;  
			}  
		} 

		//风牌  
		if(m_MyPAIVec[1].Count > 0)  
		{  
			for(int i = 0; i < m_MyPAIVec[1].Count; i++)
			{  
				switch(m_MyPAIVec[1][i])  
				{  
				case 1:  
					msg +="[ 东]";
					break;  
				case 2:
					msg +="[ 南]";
					break;  
				case 3:  
					msg +="[ 西]"; 
					break;  
				case 4:  
					msg +="[ 北]"; 
					break;  
				}  
				icount++;  
			}  
		}  

		//万  
		if(m_MyPAIVec[2].Count > 0)  
		{  
			for(int i = 0; i < m_MyPAIVec[2].Count; i++)  
			{  
				msg += "[" + m_MyPAIVec[2][i] + "万]";
				icount++;  
			}  
		}  

		//条  
		if(m_MyPAIVec[3].Count > 0)  
		{  
			for(int i = 0; i < m_MyPAIVec[3].Count; i++)  
			{  
				msg += "[" + m_MyPAIVec[3][i] + "条]";
				icount++;  
			}  
		}  

		//饼  
		if(m_MyPAIVec[4].Count > 0)  
		{  
			for(int i = 0; i < m_MyPAIVec[4].Count; i++)  
			{  
				msg += "[" + m_MyPAIVec[4][i] + "饼]";
				icount++;  
			}  
		}  

		Debug.Log(msg);
	}  

	//对一张牌进行输出  
	public void PrintPai(int p_Type, int p_Value)  
	{  
		string msg = "";

		//箭牌  
		if(p_Type==0)  
		{  
			switch(p_Value)  
			{  
			case 1:  
				msg += "[中]";  
				break;  
			case 2:  
				msg += "[发]";  
				break;  
			case 3:  
				msg += "[白]";  
				break;  
			}  
		}  
		//风牌  
		if(p_Type==1)  
		{  
			switch(p_Value)  
			{  
			case 1:  
				msg += "[东]";  
				break;  
			case 2:  
				msg += "[南]";  
				break;  
			case 3:  
				msg += "[西]";  
				break;  
			case 4:  
				msg += "[北]";  
				break;  
			}  
		}  
		//万  
		if(p_Type==2)  
		{  
			msg += "["+p_Value+"万]";  
		}  
		//条  
		if(p_Type==3)  
		{  
			msg += "["+p_Value+"条]";  
		}  
		//饼  
		if(p_Type==4)  
		{  
			msg += "["+p_Value+"饼]";  
		}  
	}  
}
