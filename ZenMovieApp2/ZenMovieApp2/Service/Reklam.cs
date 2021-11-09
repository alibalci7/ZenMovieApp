using MarcTron.Plugin;

namespace ZenMovieApp2.Service
{
    public class Reklam
    {
        public static void ReklamDoldur()
        {
            if (!CrossMTAdmob.Current.IsInterstitialLoaded())
            {
                CrossMTAdmob.Current.UserPersonalizedAds = true;
                CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
            }            
        }

        public static void ReklamGoster()
        {
            if (CrossMTAdmob.Current.IsInterstitialLoaded())
            {
                CrossMTAdmob.Current.ShowInterstitial();
            }            
        }
  
    }
   
}
