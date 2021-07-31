using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayer : PlayerPiece
{
   
   public RollingDice rednrollingDice ;


    void Start ()
    {
        rednrollingDice =  GetComponentInParent<RedHome>().rollingDice ;
    }
  

 



  public void OnMouseDown ()
      {

 
        if (GameManager.gm.rolllingDice != null)
        {

         if (!isReady)
          {  
        
           if (GameManager.gm.rolllingDice == rednrollingDice && GameManager.gm.numberofStepsToMove == 6)
           {

                GameManager.gm.redOutPlayer += 1 ;

                 MakePlayerReadyToMove(pathObjectParent.redplayerPathPoint);
                 GameManager.gm.numberofStepsToMove = 0 ;
           return ;


           }
          }

          
          if(GameManager.gm.rolllingDice == rednrollingDice && isReady &&  GameManager.gm.canPlayerMove) 
          {
        
           GameManager.gm.canPlayerMove = false ;

             MoveStep(pathObjectParent.redplayerPathPoint) ;



          }








        }


       }
















}
