using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 웹 에러
/// </summary>
public class UGSWebError : System.Exception
{
    public UGSWebError(string message) : base(message)
    {
    }
}