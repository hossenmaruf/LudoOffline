using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
   


    public PathObjectParent pathObjectParent ;
    
     public List<PlayerPiece> PlayerPieceList = new List<PlayerPiece>() ;
      PathPoint[] pathPointToMoveOn_ ;

          public UiManager uiManager ;  
     
   


   void Start ()
   {
        pathObjectParent = GetComponentInParent<PathObjectParent>() ;
          // uiManager = GetComponent<UiManager>() ;
          uiManager =FindObjectOfType<UiManager>() ;

   }





     public bool  AddPlayerPice(PlayerPiece playerPiece_) 
     {  

       if (this.name == "Centralpath") 
       {
         Completed(playerPiece_) ;  

       }
         
       if (this.name != "pathPoint (3)" && this.name != "pathPoint (42)" && this.name != "pathPoint (41)" && this.name != "pathPoint (27)" && this.name != "pathPoint (32)" && this.name != "pathPoint (65)" && this.name != "pathPoint (66)" && this.name != "pathPoint (8)" && this.name != "Centralpath")
       {
           
       if (PlayerPieceList.Count == 1)
       {
         string preePlayerPieceName = PlayerPieceList[0].name ;
          string currtPlayerPiceName = playerPiece_.name ;
          currtPlayerPiceName = currtPlayerPiceName.Substring(0, currtPlayerPiceName.Length - 4) ;

             if (!preePlayerPieceName.Contains(currtPlayerPiceName))
             {
                    FindObjectOfType<AudioManager>().Play("kills") ;
                   PlayerPieceList[0].isReady = false ;

                    StartCoroutine (revertOnStart(PlayerPieceList[0])) ;

                    PlayerPieceList[0].numberOfStepsAlredyMove = 0 ;
               
                    RemovePlayerPiece(PlayerPieceList[0]) ;
                    PlayerPieceList.Add(playerPiece_) ;
                   return false ;
             }

         }
       }


      AddPlayer(playerPiece_) ;
      return true ;


     }

         IEnumerator revertOnStart(PlayerPiece playerPce_)
          {
                 
             if (playerPce_.name.Contains("blue")) {GameManager.gm.blueOutPlayer -= 1 ; pathPointToMoveOn_ = pathObjectParent.bluePlayerPathPoint ;}
                else  if (playerPce_.name.Contains("red")) {GameManager.gm.redOutPlayer -= 1 ;pathPointToMoveOn_ = pathObjectParent.redplayerPathPoint ;}
                  else  if (playerPce_.name.Contains("yellow")) {GameManager.gm.yellowOutPlayer -= 1 ; pathPointToMoveOn_ = pathObjectParent.yellowPlayerPathpoint ;}
                    else  if (playerPce_.name.Contains("green")) {GameManager.gm.greenOutPlayer -= 1 ;pathPointToMoveOn_ = pathObjectParent.greenPlayerPathPoint ;}
                
                  for (int i = playerPce_.numberOfStepsAlredyMove ; i >= 0 ; i--) //-1
                  {
                    playerPce_.transform.position = pathPointToMoveOn_[i].transform.position ;  //something wrong
                    yield return new WaitForSeconds(.05f) ;
                  }
               
               playerPce_.transform.position = pathObjectParent.BasePoint[BasePointPosition(playerPce_.name)].transform.position ;
             
          }
    

          int BasePointPosition (string name)
          {


             for (int i =0 ; i < pathObjectParent.BasePoint.Length ; i++)
             {
               if (pathObjectParent.BasePoint[i].name == name)
               {
                 return i ;
               }
             }

           return -1 ;


          }











       void AddPlayer (PlayerPiece playerPiece_)
      {
    

       PlayerPieceList.Add(playerPiece_) ;
       ReScaleAndRePositionAllPlayerPice() ;
        

   

      }




      public void RemovePlayerPiece(PlayerPiece playerPce_)
      {
       if(PlayerPieceList.Contains(playerPce_))
       {
           PlayerPieceList.Remove(playerPce_) ;
           ReScaleAndRePositionAllPlayerPice() ;
       }
      }
                     
        public void Completed (PlayerPiece playerPce_)
         {
             
            FindObjectOfType<AudioManager>().Play("finish") ;

             if (playerPce_.name.Contains("blue")) {GameManager.gm.blueCompletedPlayer += 1; GameManager.gm.blueOutPlayer -= 1 ; if(GameManager.gm.blueCompletedPlayer == 4) {showCelebrationWindows();} }
                else  if (playerPce_.name.Contains("red")) {GameManager.gm.redCompletedPlayer += 1 ;  GameManager.gm.redOutPlayer -= 1 ;if(GameManager.gm.redCompletedPlayer == 4) {showCelebrationWindows() ;} }
                  else  if (playerPce_.name.Contains("yellow")) {GameManager.gm.yellowCompletedPlayer += 1 ;  GameManager.gm.yellowOutPlayer -= 1 ;if(GameManager.gm.yellowCompletedPlayer == 4) {showCelebrationWindows () ;}  }
                    else  if (playerPce_.name.Contains("green")) {GameManager.gm.greenCompletedPlayer += 1 ;  GameManager.gm.greenOutPlayer -= 1 ;if(GameManager.gm.greenCompletedPlayer == 4) {showCelebrationWindows() ;}  }

                Debug.Log("i am not working") ;
         }




      public  void showCelebrationWindows ()
      { 

        uiManager.gameOverPanel.SetActive(true) ;
        
            }






    public void ReScaleAndRePositionAllPlayerPice ()
    {


     int plsCount = PlayerPieceList.Count;
      bool isOdd = (plsCount % 2) == 0 ? false:true ;

         int extent = plsCount / 2 ;
         int counter =  0 ; 
           int spriteLayer = 0 ;
                                      
      
       if (isOdd)
       {
           for (int i = -extent; i <= extent ; i++)
           {
               PlayerPieceList[counter].transform.localScale = new Vector3 (pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount -1] , 1f) ;
               PlayerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount -1]), transform.position.y , 0f) ;
               counter++ ;
           }
       }

      else{

             for (int i = -extent; i < extent ; i++)
           {
               PlayerPieceList[counter].transform.localScale = new Vector3 (pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount -1] , 1f) ;
               PlayerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount -1]), transform.position.y , 0f) ;
               counter++ ;
           }

      }

  


      for (int i = 0 ; i< PlayerPieceList.Count; i++ )
      {
        PlayerPieceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayer ;
        spriteLayer++ ;

      }
     





    }
   



}
