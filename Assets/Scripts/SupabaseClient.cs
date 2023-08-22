using System.Collections.Generic;
using System.Threading.Tasks;
using Supabase.Gotrue;
using Postgrest.Responses;
using Client = Supabase.Client;
using UnityEngine.Profiling;
using System;
using UnityEngine;

public class SupabaseClient
{
    private static string SUPABASE_URL = "https://jgwyqqbwmjifiyoxugpv.supabase.co";
    private static string SUPABASE_PUBLIC_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Impnd3lxcWJ3bWppZml5b3h1Z3B2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTIxMDU0MzcsImV4cCI6MjAwNzY4MTQzN30.W4ZCX9mfdhcTm8BjgaCvmXJIHnUuK_pUpFLbfRjontI";
    private static readonly Client _instance;

    public static Client Instance => _instance;

    static SupabaseClient()
    {
        if (_instance == null)
        {
            _instance = new Client(SUPABASE_URL, SUPABASE_PUBLIC_KEY);
            _instance.InitializeAsync().Wait();
        }
    }

    public static async Task<Session> SignUpUser(string email, string password, string phone, string firstName, string lastName, int highscore)
    {
        // Create user metadata
        SignUpOptions options = new SignUpOptions
        {
            FlowType = Constants.OAuthFlowType.Implicit,
            Data = new Dictionary<string, object>
            {
                { "email", email },
                { "phone", phone },
                { "first_name", firstName },
                { "last_name", lastName },
                { "highscore", highscore }
            }
        };

        // Sign up
        return await Instance.Auth.SignUp(email, password, options);
    }

    public static async Task<PriceInfo> GetPriceInfo()
    {
        BaseResponse response = await Instance.Rpc("get_price", null);
        return JsonUtility.FromJson<PriceInfo>(response.Content);
    }
}