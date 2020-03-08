using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModel
{
    // clase que es la copia de la estructura de la base de datos .
    public  System.UInt64 id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public System.DateTime created_at { get; set; }
}
