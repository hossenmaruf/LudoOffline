using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayer : PlayerPiece
{
   

  RollingDice greenrollingDice ;


    void Start ()
    {
        greenrollingDice =  GetComponentInParent<GreenHome>().rollingDice ;
    }
  


     public void OnMouseDown ()
      {

 
        if (GameManager.gm.rolllingDice != null)
        {

         if (!isReady)
          {  
        
           if (GameManager.gm.rolllingDice == greenrollingDice && GameManager.gm.numberofStepsToMove == 6)
           {
              GameManager.gm.greenOutPlayer += 1 ;

                 MakePlayerReadyToMove(pathObjectParent.greenPlayerPathPoint);
                 GameManager.gm.numberofStepsToMove = 0 ;
           return ;


           }
          }

          
          if(GameManager.gm.rolllingDice == greenrollingDice && isReady && GameManager.gm.canPlayerMove ) 
          {
        
            GameManager.gm.canPlayerMove = false ;

            MoveStep(pathObjectParent.greenPlayerPathPoint) ;


          }


        }


       }



















}
