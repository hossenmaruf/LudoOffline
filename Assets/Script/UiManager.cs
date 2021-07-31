using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;

public class UiManager : MonoBehaviour
{
   


   public GameObject MainPanel ;
      public GameObject GamePanel ;

        public GameObject gameOverPanel ;
         
         public Button myButton ;

        
            
    
          
      


      public void Game1 ()
      {   
        GameManager.gm.totalPlayerCanPlay = 2 ;
        
          Game1Setting() ;
        
          Activation() ;
           
      }


    public void Game2 ()
      {
        GameManager.gm.totalPlayerCanPlay = 3 ;
        Activation() ;
         Game2Setting() ;
      


      }


   public void Game3 ()
      {
          GameManager.gm.totalPlayerCanPlay = 4 ;
          Activation() ;


      }


    public void Game4 ()
      {


           GameManager.gm.totalPlayerCanPlay = 1 ;
           Activation() ;

            Game1Setting() ;
           

      }

             public void Activation ()
             {
                myButton.gameObject.SetActive(true) ;
                 MainPanel.SetActive(false) ;
                 GamePanel.SetActive(true) ;     
                  AdManager.instance.ShowRewardedAd() ;
                  
             }

   void Game1Setting ()
   {
     HidePlayers (GameManager.gm.redPlayerPice) ;
     HidePlayers(GameManager.gm.greenPlayerPice) ;
   }

    void Game2Setting ()
   {
     HidePlayers(GameManager.gm.greenPlayerPice) ;
   }
  
    void HidePlayers (PlayerPiece[] playerPieces_)
    {
      for (int i = 0 ; i < playerPieces_.Length; i++)
      {
        playerPieces_[i].gameObject.SetActive(false) ;
       }
    }



    public void menu ()
    {
              

               MainPanel.SetActive(true) ;
                 gameOverPanel.SetActive(false) ;
                    GamePanel.SetActive(false) ;
               
                 AdManager.instance.ShowFullScreenAd() ;
               
            Time.timeScale = 1f ; 
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex) ;
            //  Time.timeScale = 1f ; 
     

    }


               public void continous ()
             {
                 gameOverPanel.SetActive(false) ;
                  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex) ;
                   Game1() ;
                    AdManager.instance.ShowFullScreenAd() ;
                


             }

   


    public void gamePanelMenu ()
    {
      GamePanel.SetActive(false) ;
      MainPanel.SetActive(true) ;
       
    }

              public void QuitGame()
           {
              Application.Quit() ;
            }





}
