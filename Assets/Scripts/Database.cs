using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Supabase.Gotrue;
using Client = Supabase.Client;

public class Database : MonoBehaviour
{

    private static string SUPABASE_URL = "https://jgwyqqbwmjifiyoxugpv.supabase.co";
    private static string SUPABASE_PUBLIC_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Impnd3lxcWJ3bWppZml5b3h1Z3B2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTIxMDU0MzcsImV4cCI6MjAwNzY4MTQzN30.W4ZCX9mfdhcTm8BjgaCvmXJIHnUuK_pUpFLbfRjontI";
    private static readonly Client _instance;
    public static Client Instance => _instance;

    static Database()
    {
        if (_instance == null)
        {
            _instance = new Client(SUPABASE_URL, SUPABASE_PUBLIC_KEY);
            _instance.InitializeAsync().Wait();
        }
        Debug.Log(_instance);
    }

   //public static async void SetHighscore(int highscore)
   //{
   //    try
   //    {
   //        await Instance
   //            .From<Prize>()
   //            .Where(x => x.Id == Instance.Auth.CurrentSession.User.Id)
   //            .Set(x => x.Amount, )
   //            .Update();
   //    }
   //    catch (Exception)
   //    {
   //        throw;
   //    }
   //}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


