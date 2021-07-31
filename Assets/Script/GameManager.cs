using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public int numberofStepsToMove ;
    public RollingDice rolllingDice ;
    public bool canPlayerMove = true ;
     

   public static GameManager gm ;

          List<PathPoint> playerOnPathpointList = new List<PathPoint>() ;
           
           

             public bool canDiceRoll = true ;
             public bool transferDice = false ;
             public bool selffDice = false  ;




          public int blueOutPlayer ;
           public int redOutPlayer ;
            public int greenOutPlayer ;
             public int yellowOutPlayer ;


                
          public int blueCompletedPlayer ;
           public int redCompletedPlayer ;
            public int greenCompletedPlayer ;
             public int yellowCompletedPlayer ;



               public RollingDice[] manageRollingDice ;
                
                 public PlayerPiece[] bluePlayerPice ;
                  public PlayerPiece[] redPlayerPice ;
                   public PlayerPiece[] yellowPlayerPice  ;
                    public PlayerPiece[] greenPlayerPice ;
                 

                 public int totalPlayerCanPlay ;
                   public int totalSix ;








           public void Awake ()
          {
             gm = this ;
      

          }


     


      public void AddPathPoint (PathPoint pathPoint)
      {
          playerOnPathpointList.Add(pathPoint) ;
      }  

      public void RemovePathPoint (PathPoint pathPoint)
      {
          if (playerOnPathpointList.Contains(pathPoint))
          {
              playerOnPathpointList.Remove(pathPoint) ;
          }
          else 
          {
              Debug.Log ("path Point is not found") ;
          }
      }




     public void rollingDiceManager ()
     {


        if (GameManager.gm.transferDice)
        {     
          GameManager.gm.canDiceRoll = true ;

             if (GameManager.gm.numberofStepsToMove !=6) 
                   {

                     SiftDice () ;

                   }
               
        
         }
    
         else 
          {
              if(GameManager.gm.selffDice)
               {
                   GameManager.gm.selffDice = false ;
                   GameManager.gm.canDiceRoll = true ;
                   GameManager.gm.SelfRoll() ;
               }
          }
      
    }

         public void SelfRoll ()
         {
              if (GameManager.gm.totalPlayerCanPlay == 1 && GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[2])
              {
                  Invoke("roled" , 0.6f) ;
              }
         }

        void roled ()
        {
            GameManager.gm.manageRollingDice[2].mouseRoll() ;
        }
 




     void SiftDice()
     {
          int nextDice ;

         if (GameManager.gm.totalPlayerCanPlay == 1)
          {
             
                if (GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[0]) 
                {
                     GameManager.gm.manageRollingDice[0].gameObject.SetActive(false) ;
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(true) ;
                    passout (0) ;
                    GameManager.gm.manageRollingDice[2].mouseRoll() ;

                }
              else 
                {
                    
                     GameManager.gm.manageRollingDice[0].gameObject.SetActive(true) ;
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(false) ;
                            passout (2) ;

                }


           }
         else if (GameManager.gm.totalPlayerCanPlay == 2)
          {
              if (GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[0]) 
              {
                     GameManager.gm.manageRollingDice[0].gameObject.SetActive(false) ;
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(true) ;
                    passout (0) ;

              }
              else 
              {
                   
                     GameManager.gm.manageRollingDice[0].gameObject.SetActive(true) ;
                    GameManager.gm.manageRollingDice[2].gameObject.SetActive(false) ;
                    passout (2) ;

              }
              

          

         }
         else if (GameManager.gm.totalPlayerCanPlay == 3)
         {

          for (int i = 0 ; i< 3 ; i++)
            {
                if(i == 2) {nextDice = 0 ;} else {nextDice = i + 1 ;}
                 i = passout(i) ;

                if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[i])
                {

                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false) ;
                    GameManager.gm.manageRollingDice[nextDice].gameObject.SetActive(true) ;
                }
            }
         }
         else 
         {

            for (int i = 0 ; i < 4 ; i++)
             {
                if(i == 3) {nextDice = 0 ;} else {nextDice = i + 1 ;}

                i = passout(i) ;


                if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[i])
                {

                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false) ;
                    GameManager.gm.manageRollingDice[nextDice].gameObject.SetActive(true) ;
                }
             }
         }

 
     }
          
           



            int passout (int i)
             {
                 if (i== 0) {if(GameManager.gm.blueCompletedPlayer == 4) {return (i +1) ;}}
                else if (i== 1) {if(GameManager.gm.blueCompletedPlayer == 4) {return (i +1) ;}}
                else if (i==2) {if(GameManager.gm.blueCompletedPlayer == 4) {return (i +1) ;}}
               else if (i==3) {if(GameManager.gm.blueCompletedPlayer == 4) {return (i +1) ;}}

                return i ;
             }




}
