using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Data;
using System.Text;

// using System.Collections;
// using System.Collections.Generic;
// using System.Security.Cryptography;

using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Reflection;

public class TestingController : MonoBehaviour
{
	
	string host ="localhost";
     string database = "unitydd";
     string user = "root";
     string password = "";
    private string connectionString;
    private MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private IDataReader rdr = null;
	// private MD5 _md5Hash;
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		connectionString = "Server=" + host + ";Database=" + database + ";User=" + user + ";Password=" + password + ";CharSet=utf8;Pooling=";
		con = new MySqlConnection(connectionString);
		con.Open();
		this.getusers();
	}
	public  List<T> DataReaderMapToList<T>(string sql)
	{
		cmd = new MySqlCommand(sql, this.con);
		rdr = cmd.ExecuteReader();
		List<T> list = new List<T>();
		T obj = default(T);
		while (rdr.Read())
		{
			obj = Activator.CreateInstance<T>();
			foreach (PropertyInfo prop in obj.GetType().GetProperties())
			{
				if (!object.Equals(rdr[prop.Name], DBNull.Value))
				{
					prop.SetValue(obj, rdr[prop.Name], null);
				}
			}
			list.Add(obj);
		}
		return list;
	}
	void getusers()
	{
		string sql = "SELECT * FROM users";
		try
		{
			List<UserModel> personList = new List<UserModel>();
			personList = DataReaderMapToList<UserModel>(sql);
			Debug.Log(personList);
			Debug.Log(personList[0].id+"  "+personList[0].name);
		}
		catch (System.Exception e)
		{
			Debug.Log(e);
		}
	}
	void onApplicationQuit()
	{
		if (con != null)
		{
			if (con.State.ToString() != "Closed")
			{
				con.Close();
				Debug.Log("Mysql connection closed");
			}
			con.Dispose();
		}
	}


}
