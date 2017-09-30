using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;

//剩余牌墙信息; 
//扩展;  
public struct stPAIEx  
{  
	public stPAI   m_NewPai;                       //起的新牌;  
	public int     m_PaiNum;                       //剩余牌数; 
	public bool    m_IsHZ;                         //是否黄庄;  
}  
;  

//麻将管理器; 
public class CMJManage  
{  
	List<stPAI> m_MJVec = new List<stPAI>(); //麻将数据VEC;  
	int m_HZPaiNum; //黄庄的牌数;

	public CMJManage()
	{
		m_HZPaiNum = 0;
	}

	//初始化牌;
	public void InitPai(int p_HZPaiNum)  
	{  
		m_HZPaiNum = p_HZPaiNum;  
		m_MJVec.Clear();  

		//中发白;  
		for(int i = 1 ; i <= 3 ; i++)  
		{  
			stPAI t_Pai;  
			t_Pai.m_Type = 0;  
			t_Pai.m_Value = i;  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
		}  

		//东南西北; 
		for(int i = 1 ; i <= 4 ; i++)  
		{  
			stPAI t_Pai;  
			t_Pai.m_Type = 1;  
			t_Pai.m_Value = i;  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
		} 

		//万;  
		for(int i = 1 ; i <= 9 ; i++)  
		{  
			stPAI t_Pai;  
			t_Pai.m_Type = 2;  
			t_Pai.m_Value = i;  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
		}  

		//条;  
		for(int i = 1 ; i <= 9 ; i++)  
		{  
			stPAI t_Pai;  
			t_Pai.m_Type = 3;  
			t_Pai.m_Value = i;  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
		} 

		//饼;  
		for(int i = 1 ; i <= 9 ; i++)  
		{  
			stPAI t_Pai;  
			t_Pai.m_Type = 4;  
			t_Pai.m_Value = i;  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
			m_MJVec.Add(t_Pai);  
		}

		XiPai();  
	}  

	//洗牌; 
	private void XiPai()  
	{  
		random_shuffle_RNGCryptoServiceProvider_Pro();
	} 

	// pseudo-random number generator, using a time-dependent default seed value;
	// 直接以Random做为随机数生成器因为时钟精度问题，在一个小的时间段内会得到同样的伪随机数序列，你shuffle后会得到同一个结果;
	private void random_shuffle_System_Random()
	{
		System.Random random = new System.Random();
		for (int i = 0; i < m_MJVec.Count; ++i)
		{
			int var = random.Next(0, m_MJVec.Count);
			stPAI temp = m_MJVec[i];
			m_MJVec[i] = m_MJVec[var];
			m_MJVec[var] = temp;
		}
	}

	// using a RNGCryptoServiceProvider().GetHashCode() seed value;
	// .net提供了RNGCryptoServiceProvider可以避免在一小段时间问题;
	private void random_shuffle_RNGCryptoServiceProvider()
	{
		System.Random random = new System.Random(new RNGCryptoServiceProvider().GetHashCode());
		for (int i = 0; i < m_MJVec.Count; ++i)
		{
			int var = random.Next(0, m_MJVec.Count);
			stPAI temp = m_MJVec[i];
			m_MJVec[i] = m_MJVec[var];
			m_MJVec[var] = temp;
		}
	}

	private void random_shuffle_RNGCryptoServiceProvider_Pro()
	{
		byte[] randomBytes = new Byte[4];
		RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		for (int i = 0; i < m_MJVec.Count; ++i)
		{
			rng.GetNonZeroBytes(randomBytes);
			int randomSeed = (randomBytes[0] << 24) | (randomBytes[1] << 16) | (randomBytes[2] << 8) | randomBytes[3];
			int var = randomSeed % m_MJVec.Count;
			if (var < 0) var *= -1;

			stPAI temp = m_MJVec[i];
			m_MJVec[i] = m_MJVec[var];
			m_MJVec[var] = temp;
		}
	}

	//起牌; 
	public stPAIEx GetAPai()
	{  
		//如果所有牌都起完了;
		stPAIEx t_Pai;  
		t_Pai.m_PaiNum = m_MJVec.Count - 1;  
		t_Pai.m_NewPai.m_Type = m_MJVec[m_MJVec.Count - 1].m_Type;  
		t_Pai.m_NewPai.m_Value = m_MJVec[m_MJVec.Count - 1].m_Value;  
		if(t_Pai.m_PaiNum ==m_HZPaiNum)  
		{  
			t_Pai.m_IsHZ = true;  
		}  
		else  
		{  
			t_Pai.m_IsHZ = false;  
		}  

		//扔去一个; 
		m_MJVec.RemoveAt(m_MJVec.Count - 1);  
		return t_Pai;  
	}  
};
