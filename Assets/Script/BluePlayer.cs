using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayer : PlayerPiece
{
    // Start is called before the first frame update
   
    RollingDice blueRollingDice ;
    
      

    public void Start ()
    {
        blueRollingDice = GetComponentInParent<BlueHome>().rollingDice ;
           
    }


  

      public void OnMouseDown ()
      {

 
        if (GameManager.gm.rolllingDice != null)
        {

         if (!isReady)
          {  
        
           if (GameManager.gm.rolllingDice == blueRollingDice && GameManager.gm.numberofStepsToMove == 6)
           {
                GameManager.gm.blueOutPlayer += 1 ;
                 MakePlayerReadyToMove(pathObjectParent.bluePlayerPathPoint);
                 GameManager.gm.numberofStepsToMove = 0 ;
              
           return ;


           }
          }

          
          if(GameManager.gm.rolllingDice == blueRollingDice && isReady && GameManager.gm.canPlayerMove) 
          {
           
           GameManager.gm.canPlayerMove = false ;
         MoveStep(pathObjectParent.bluePlayerPathPoint) ;
         
        


          }




        }


       }










      }




     






   









