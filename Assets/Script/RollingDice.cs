using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;

public class RollingDice : MonoBehaviour
{

     public Sprite[]  numberSprite ;
      public SpriteRenderer numberSpriteHolder ;
              public SpriteRenderer rollingDiceAnimation ; 
               public AudioManager audioManager ;
             

       public int numberGot ;
         
         Coroutine generateRandomNumber ;
         public int outPieces ;
          PathObjectParent pathParent ;
           PlayerPiece[] correntplayerpices ;
            PathPoint[] pathPointToMoveOn_ ;
              Coroutine MovePlayerPiece ;
              PlayerPiece outPlayerPice ;
                    int maxNumber = 6 ;
                      private GameObject thisRollindDice ;


           public void Awake ()
           {
             pathParent = FindObjectOfType<PathObjectParent>() ;
                   audioManager = FindObjectOfType<AudioManager>() ;
                     
                  
           }
          
          public void Start ()
          {
              thisRollindDice = this.gameObject;
          }

           
      

        public void OnMouseDown ()
        {
           
           if (EventSystem.current.IsPointerOverGameObject())
           {  
             return ;
           }
            
            if (thisRollindDice != null ) 
            {
                  
               generateRandomNumber =  StartCoroutine(rollingDice_Enum()) ;
            audioManager.Play("dice");


            }
       
         
              
        }

       public void mouseRoll ()
    {
         
            
                  generateRandomNumber =  StartCoroutine(rollingDice_Enum()) ; 
         
    }


     IEnumerator rollingDice_Enum()
     {
  
       
            GameManager.gm.transferDice = false ;
                yield return new WaitForEndOfFrame() ;


          if(GameManager.gm.canDiceRoll)
          {
              GameManager.gm.canDiceRoll = false ;
             numberSpriteHolder.gameObject.SetActive(false)  ;
             rollingDiceAnimation.gameObject.SetActive(true) ;

              yield return new WaitForSeconds(.5f) ;


                if (GameManager.gm.totalSix == 2) {  maxNumber = 5 ;}  ;
                numberGot = Random.Range (0,maxNumber) ;
                 if (numberGot == 6) {GameManager.gm.totalSix += 1 ;}   else {GameManager.gm.totalSix = 0 ;}

             numberSpriteHolder.sprite = numberSprite[numberGot] ;
             numberGot += 1 ;
 
               GameManager.gm.numberofStepsToMove = numberGot ;
               GameManager.gm.rolllingDice = this ;

 
              numberSpriteHolder.gameObject.SetActive(true)  ;
              rollingDiceAnimation.gameObject.SetActive(false) ;
               yield return new WaitForEndOfFrame() ;

              int nummberGot = GameManager.gm.numberofStepsToMove ;  

            if (PlayerCanNotMove())
            {
              yield return new WaitForSeconds(.5f) ;
              if (nummberGot != 6) { GameManager.gm.transferDice = true ; }
              else {GameManager.gm.selffDice = true ;}

            }
            else 

              {

                 if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[0]) {outPieces = GameManager.gm.blueOutPlayer ;}
                   else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[1]) {outPieces = GameManager.gm.redOutPlayer ;}
                        else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[2]) {outPieces = GameManager.gm.yellowOutPlayer ;}
                                  else  if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[3]) {outPieces = GameManager.gm.greenOutPlayer ;}

    
                    if (outPieces == 0 && nummberGot != 6)
                    {
                     yield return new WaitForSeconds(.3f) ;
                      GameManager.gm.transferDice = true ;
                    }
                    else 
                      {
                         if(outPieces == 0 && nummberGot == 6 )
                         {
                           MakePlayerReadyToMove(0) ;
                          }
                         else if(outPieces == 1 && nummberGot !=6 && GameManager.gm.canPlayerMove)
                          {
                         
                         int playerPicePosition = CheckoutPlayer () ;

                           if (playerPicePosition >= 0)
                            {
                               GameManager.gm.canPlayerMove = false ;
                              MovePlayerPiece = StartCoroutine(MoveSteps_Enum(playerPicePosition)) ;

                            }
                         else
                         {
                            yield return new WaitForSeconds(.5f) ;
                            if (nummberGot != 6) { GameManager.gm.transferDice = true ; }
                           else {GameManager.gm.selffDice = true ;}
                         }

    
                      }
 
                   else if (GameManager.gm.totalPlayerCanPlay == 1 && GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[2])

                     {
                         if (nummberGot == 6 && outPieces < 4)
                          {
                            MakePlayerReadyToMove(outPlayerToMove()) ;
                          }

                         else 
                         {   
                           int playerPicePosition = CheckoutPlayer () ;

                           if (playerPicePosition >= 0)
                           {
                             GameManager.gm.canPlayerMove = false ;
                             MovePlayerPiece = StartCoroutine(MoveSteps_Enum(playerPicePosition)) ;
  
                            }
                             else 
                           {
                              yield return new WaitForSeconds(.5f) ;
                              if (nummberGot != 6)
                               { GameManager.gm.transferDice = true ; }
                             else {GameManager.gm.selffDice = true ;}
                           }

                      }
                     }

                  else
                  {
                       if (CheckoutPlayer() < 0)
                       {
                           yield return new WaitForSeconds(.5f) ;
                            if (nummberGot != 6) { GameManager.gm.transferDice = true ; }
                           else {GameManager.gm.selffDice = true ;}
                       }
                  }

                 }

               }

                 GameManager.gm.rollingDiceManager() ;
             

            if (generateRandomNumber != null)
            {
                StopCoroutine(rollingDice_Enum()) ;
            }

          
          }

    }
     
          
          int outPlayerToMove ()
          {
            for (int i = 0 ; i < 4 ; i++)
            {
              if (!GameManager.gm.yellowPlayerPice[i].isReady)
              {
                return i ;
              }
            }
            return 0 ;
          }


       int CheckoutPlayer ()
       {

           if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[0]) {correntplayerpices = GameManager.gm.bluePlayerPice; pathPointToMoveOn_ = pathParent.bluePlayerPathPoint ;}
                     else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[1]) {correntplayerpices = GameManager.gm.redPlayerPice; pathPointToMoveOn_ = pathParent.redplayerPathPoint  ;}
                        else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[2]) {correntplayerpices = GameManager.gm.yellowPlayerPice; pathPointToMoveOn_ = pathParent.yellowPlayerPathpoint  ;}
                            else  if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[3]) {correntplayerpices = GameManager.gm.greenPlayerPice; pathPointToMoveOn_ = pathParent.greenPlayerPathPoint  ;}


               
        for (int i = 0 ; i < correntplayerpices.Length ; i++)
        {


              if ( correntplayerpices[i].isReady && isPathPointAvaialeToMove( GameManager.gm.numberofStepsToMove, correntplayerpices[i].numberOfStepsAlredyMove ,  pathPointToMoveOn_  ))
                   {
                        
                        return i ;
                      
                   }
        }

       return -1 ;

  }

        
        public bool PlayerCanNotMove ()
        {
          
           if (outPieces > 0)
           {

           bool canNotMove = false ;

                   if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[0]) {correntplayerpices = GameManager.gm.bluePlayerPice; pathPointToMoveOn_ = pathParent.bluePlayerPathPoint ;}
                     else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[1]) {correntplayerpices = GameManager.gm.bluePlayerPice; pathPointToMoveOn_ = pathParent.bluePlayerPathPoint  ;}
                        else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[2]) {correntplayerpices = GameManager.gm.bluePlayerPice; pathPointToMoveOn_ = pathParent.bluePlayerPathPoint  ;}
                            else  if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[3]) {correntplayerpices = GameManager.gm.bluePlayerPice; pathPointToMoveOn_ = pathParent.bluePlayerPathPoint  ;}



               for (int i = 0 ; i < correntplayerpices.Length ; i++)
               {
                 if (correntplayerpices[i].isReady)
                 {
                   if (isPathPointAvaialeToMove( GameManager.gm.numberofStepsToMove, correntplayerpices[i].numberOfStepsAlredyMove ,  pathPointToMoveOn_  ))
                   {
                     return  false ;
                   }
                 }

                 else 
                 {
                   if(!canNotMove) {canNotMove = true ;}
                 }

               }

          if (canNotMove)
          {
            return true ;
          }
          }


       return false ;



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
     



       public void MakePlayerReadyToMove (int outPlayer)
       {
                //check again
                  
                  GameManager.gm.transferDice = false ;
              
             if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[0]) {outPlayerPice = GameManager.gm.bluePlayerPice[outPlayer]; pathPointToMoveOn_= pathParent.bluePlayerPathPoint; GameManager.gm.blueOutPlayer += 1 ;}
               else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[1]) {outPlayerPice = GameManager.gm.redPlayerPice[outPlayer]; pathPointToMoveOn_= pathParent.redplayerPathPoint; GameManager.gm.redOutPlayer += 1  ;}
                      else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[2]) {outPlayerPice = GameManager.gm.yellowPlayerPice[outPlayer]; pathPointToMoveOn_= pathParent.yellowPlayerPathpoint; GameManager.gm.yellowOutPlayer += 1  ;}
                                  else  if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[3]) {outPlayerPice = GameManager.gm.greenPlayerPice[outPlayer]; pathPointToMoveOn_= pathParent.greenPlayerPathPoint; GameManager.gm.greenOutPlayer += 1  ;}


             outPlayerPice.isReady = true ;
              


               outPlayerPice.transform.position = pathPointToMoveOn_[0].transform.position ;

             outPlayerPice.numberOfStepsAlredyMove = 1 ;

              outPlayerPice.previousPathPoint = pathPointToMoveOn_[0];
               outPlayerPice.currentPathPoint = pathPointToMoveOn_[0] ;
                         
                        FindObjectOfType<AudioManager>().Play("finish") ;


               outPlayerPice.currentPathPoint.AddPlayerPice(outPlayerPice) ;

               GameManager.gm.AddPathPoint(outPlayerPice.currentPathPoint) ;
               

                  GameManager.gm.canDiceRoll = true ;
                     GameManager.gm.selffDice = true ;
                    

              
          
          

               GameManager.gm.numberofStepsToMove = 0 ;

             GameManager.gm.SelfRoll() ;

       
    }




          public IEnumerator MoveSteps_Enum ( int movePlayer )
           {  
                 GameManager.gm.transferDice = false ;
                 yield return new WaitForSeconds(.15f) ;
                    


                if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[0]) {outPlayerPice = GameManager.gm.bluePlayerPice[movePlayer]; pathPointToMoveOn_= pathParent.bluePlayerPathPoint; }
                  else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[1]) {outPlayerPice = GameManager.gm.redPlayerPice[movePlayer]; pathPointToMoveOn_= pathParent.redplayerPathPoint;}
                      else if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[2]) {outPlayerPice = GameManager.gm.yellowPlayerPice[movePlayer]; pathPointToMoveOn_= pathParent.yellowPlayerPathpoint; }
                                  else  if(GameManager.gm.rolllingDice == GameManager.gm.manageRollingDice[3]) {outPlayerPice = GameManager.gm.greenPlayerPice[movePlayer]; pathPointToMoveOn_= pathParent.greenPlayerPathPoint;}



                int numberofStepsToMove = GameManager.gm.numberofStepsToMove ;
             
              outPlayerPice.currentPathPoint.ReScaleAndRePositionAllPlayerPice() ;

            for (int i = outPlayerPice.numberOfStepsAlredyMove ;i <(outPlayerPice.numberOfStepsAlredyMove + numberofStepsToMove); i++)
           {
                       
              if(isPathPointAvaialeToMove  ( numberofStepsToMove, outPlayerPice.numberOfStepsAlredyMove ,  pathPointToMoveOn_ ))
              {

               outPlayerPice.transform.position = pathPointToMoveOn_[i].transform.position ;
               
                audioManager.Play("movement") ; 
                yield return new WaitForSeconds(.15f) ;

              }
          }

          if(isPathPointAvaialeToMove( numberofStepsToMove, outPlayerPice.numberOfStepsAlredyMove ,  pathPointToMoveOn_ ))

          {

               
                //    GameManager.gm.canPlayerMove = true ;

             outPlayerPice.numberOfStepsAlredyMove += numberofStepsToMove ;
             

          GameManager.gm.RemovePathPoint(outPlayerPice.previousPathPoint) ;
           outPlayerPice.previousPathPoint.RemovePlayerPiece(outPlayerPice) ;

           outPlayerPice.currentPathPoint = pathPointToMoveOn_[outPlayerPice.numberOfStepsAlredyMove - 1] ;


             
            GameManager.gm.AddPathPoint(outPlayerPice.currentPathPoint) ;
           outPlayerPice.previousPathPoint = outPlayerPice.currentPathPoint ; 




  
             if (outPlayerPice.currentPathPoint.AddPlayerPice(outPlayerPice))
             {
                  if (outPlayerPice.numberOfStepsAlredyMove == 57)
                  {
                      GameManager.gm.selffDice = true ;
                    //  GameManager.gm.
                      
                     
                  }
                  else
                  {

                     if (GameManager.gm.numberofStepsToMove !=6)
                      {

                       GameManager.gm.transferDice = true ;
                         yield return new WaitForSeconds(.5f) ;
   
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
         


              GameManager.gm.numberofStepsToMove = 0 ;

          }  


              GameManager.gm.canPlayerMove = true ;
                GameManager.gm.rollingDiceManager() ;


     if (MovePlayerPiece != null)
     {
         StopCoroutine("MoveSteps_Enum") ;
     }





   }




}
