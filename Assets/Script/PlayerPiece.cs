using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    
 public bool isReady ;
  public bool moveNow ;
    
     
  public int numberofStepsToMove ;
   public int numberOfStepsAlredyMove ;

     public PathObjectParent pathObjectParent ;
        Coroutine MovePlayerPiece ;

          public PathPoint previousPathPoint ;
             public PathPoint currentPathPoint ;

           //   bool animBool = false ;
        
    
      
 

   
    public void Awake ()
    {
        pathObjectParent = FindObjectOfType<PathObjectParent>() ;
          
         
    }
        

   public void MoveStep (PathPoint[] pathPointToMoveOn_)
   { 
            MovePlayerPiece = StartCoroutine(MoveSteps_Enum(pathPointToMoveOn_)) ;

   }



    public void MakePlayerReadyToMove (PathPoint[] pathpointToMoveOn_)
      
    {
         

         

              isReady =true ;


               

           transform.position = pathpointToMoveOn_[0].transform.position ;

           numberOfStepsAlredyMove = 1 ;

           previousPathPoint = pathpointToMoveOn_[0];
           currentPathPoint = pathpointToMoveOn_[0] ;
            
            FindObjectOfType<AudioManager>().Play("finish") ;


           currentPathPoint.AddPlayerPice(this) ;

           GameManager.gm.AddPathPoint(currentPathPoint) ;


            GameManager.gm.canDiceRoll = true ;
            GameManager.gm.selffDice = true ;
               GameManager.gm.transferDice = false ;
       
    }




    
    public IEnumerator MoveSteps_Enum (PathPoint[] pathPointsToMoveOn_)

      {  

          GameManager.gm.transferDice = false ;

         yield return new WaitForSeconds(.25f) ;

         numberofStepsToMove = GameManager.gm.numberofStepsToMove ;

          for (int i = numberOfStepsAlredyMove ; i <(numberOfStepsAlredyMove + numberofStepsToMove); i++)
         {
          
          currentPathPoint.ReScaleAndRePositionAllPlayerPice() ;
          
          if(isPathPointAvaialeToMove  ( numberofStepsToMove, numberOfStepsAlredyMove ,  pathPointsToMoveOn_ ))
           {

            transform.position = pathPointsToMoveOn_[i].transform.position ;
            FindObjectOfType<RollingDice>().audioManager.Play("movement") ;

          
            yield return new WaitForSeconds(.15f) ;

          }
       }

          if(isPathPointAvaialeToMove( numberofStepsToMove, numberOfStepsAlredyMove ,  pathPointsToMoveOn_ ))
          {
          
                numberOfStepsAlredyMove += numberofStepsToMove  ;
                             GameManager.gm.canPlayerMove = true ;
                                 GameManager.gm.transferDice = false ;
                             

                 GameManager.gm.RemovePathPoint(previousPathPoint) ;
                  previousPathPoint.RemovePlayerPiece(this) ;

                  currentPathPoint = pathPointsToMoveOn_[numberOfStepsAlredyMove - 1] ;

                  if (currentPathPoint.AddPlayerPice(this))
                    {
                     if (numberOfStepsAlredyMove == 57)
                     {
                      GameManager.gm.selffDice = true ;
                    
                    }
                       else
                        {

                        if (GameManager.gm.numberofStepsToMove !=6)
                         {
                          GameManager.gm.transferDice = true ;
                         // yield return new WaitForSeconds(.5f) ;

                          
                        }
                          else 
                        {

                          GameManager.gm.selffDice = true ;

                        }          
               }
             }
            
            else 
            {
              GameManager.gm.selffDice = true ;
            }   
         

            GameManager.gm.AddPathPoint(currentPathPoint) ;
            previousPathPoint = currentPathPoint ;

            GameManager.gm.numberofStepsToMove = 0 ;

       }  
          
                  

             GameManager.gm.canPlayerMove = true ;
               GameManager.gm.rollingDiceManager() ;


     if (MovePlayerPiece != null)
     {
         StopCoroutine(MoveSteps_Enum(pathPointsToMoveOn_)) ;
     }





   }





         bool  isPathPointAvaialeToMove(int numOfStepstoMove_ , int numberOfStepsAllredyMove , PathPoint[] pathPointToMove_)
       {


     if (numOfStepstoMove_ == 0)
     {
       return false ;
     }

     int leftNumOfPath = pathPointToMove_.Length - numberOfStepsAllredyMove ;

      if (leftNumOfPath >= numOfStepstoMove_)
      {
         return true ;
      }

       else 
       {
         return false ;
       }

     

       }
     



}
