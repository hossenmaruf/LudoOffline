using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayer : PlayerPiece
{
    




 
    RollingDice yellowRollingDice ;

    public void Start ()
    {
        yellowRollingDice = GetComponentInParent<YellowHome>().rollingDice ;
    }

  


    
       public void OnMouseDown ()
      {

 
        if (GameManager.gm.rolllingDice != null)
        {

         if (!isReady)
          {  
        
           if (GameManager.gm.rolllingDice == yellowRollingDice && GameManager.gm.numberofStepsToMove == 6)
           {
             GameManager.gm.yellowOutPlayer += 1 ;

                 MakePlayerReadyToMove(pathObjectParent.yellowPlayerPathpoint);
                 GameManager.gm.numberofStepsToMove = 0 ;
           return ;


           }
          }

          
          if(GameManager.gm.rolllingDice == yellowRollingDice && isReady &&  GameManager.gm.canPlayerMove ) 

          {


         GameManager.gm.canPlayerMove = false ;

         MoveStep(pathObjectParent.yellowPlayerPathpoint) ;


          }








        }
      }
















}
