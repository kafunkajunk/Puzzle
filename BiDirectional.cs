using System.Diagnostics;
using System.Collections.Generic;
using System;
namespace a1{
  public class BiDirectional : Algorithm {


    //so we're gonna need two queues
    public Queue<State> FrontOpenSet = new Queue<State>();
    public Queue<State> BackOpenSet = new Queue<State>();

    // and two closed sets
    public Dictionary<string,State> FrontClosedSet = new Dictionary<string,State>();
    public Dictionary<string,State> BackClosedSet = new Dictionary<string,State>();

    public Dictionary<string,State> frontparents = new Dictionary<string,State>();
    public Dictionary<string,State> backparents = new Dictionary<string,State>();

    public BiDirectional(int[] arr){
      CurrentState = new State(arr);
    }

    public override void Execute(){
      watch = new Stopwatch();
      Console.WriteLine("Beginning execution for Bi-directional");
      Console.WriteLine("");
      watch.Start();

      FrontOpenSet.Enqueue(CurrentState);
      BackOpenSet.Enqueue(new State(GlobalVar.GOAL));
      while(FrontOpenSet.Count > 0 || BackOpenSet.Count > 0){


	//pop from queue for each direction
	var front = FrontOpenSet.Dequeue();
	var back = BackOpenSet.Dequeue();

	if(FrontClosedSet.ContainsKey(front.Key) == false) FrontClosedSet.Add(front.Key,front);
	if(BackClosedSet.ContainsKey(back.Key) == false) BackClosedSet.Add(back.Key,back);

	//check if either is at it's respective goal positions, just in case they don't ever intersect 
	if(front.IsEqualToGoal()){
	  watch.Stop();

	  Console.WriteLine("Found goal through front open set, no intersection");
	  break;


	}
	if( back.IsEqualToGoal(CurrentState.StateArray)){
	  watch.Stop();

	  Console.WriteLine("Found goal through back open set, no intersection");
	  break;
	}


	var fChildren = front.BuildChildren();
	var bChildren = back.BuildChildren();

	foreach(var child in fChildren){

	  if(FrontClosedSet.ContainsKey(child.Key)) continue;

	  FrontOpenSet.Enqueue(child);

	  if(!frontparents.ContainsKey(child.Key)) frontparents.Add(child.Key,front);
	  

	}

	foreach(var child in bChildren){
	  if(BackClosedSet.ContainsKey(child.Key)) continue;

	  BackOpenSet.Enqueue(child);

	  if(!backparents.ContainsKey(child.Key)) backparents.Add(child.Key,back);
	}


	//chech if either is in the closed set of each direction
	if(BackClosedSet.ContainsKey(front.Key) ){

	  Console.WriteLine("Found Solution");
	  SolutionFound(front);
	  //found solution
	  break;

	} 

	if( FrontClosedSet.ContainsKey(back.Key)){

	  Console.WriteLine("Found Solution");
	  SolutionFound(back);
	  //found solution
	  break;
	}

	if(BackOpenSet.Count == 0 && FrontOpenSet.Count == 0){
	  throw new Exception("no solution");
	}

      }
      watch.Stop();
    }

    private void SolutionFound(State FinalState){

      //Debugger.Break();
      var temp = FinalState;
    

      while(true){

	if(!frontparents.ContainsKey(temp.Key)) break;

	moves.AddLast(frontparents[temp.Key]);
	temp = frontparents[temp.Key];
       

      }

      temp = FinalState;
      while(true){

	if(!backparents.ContainsKey(temp.Key)) break;
	moves.AddLast(backparents[temp.Key]);
	temp = backparents[temp.Key];

      }

      Console.WriteLine("Move list: ");
      var move = moves.First;
      while(move.Next != null){
      //for(var i = 0; i < moves.Count;i++){

	Console.WriteLine( move.Value );

	move = move.Next;

      }
      if(moves.Count > 100) Console.WriteLine("More than 100 ...");

    }

  }
}
