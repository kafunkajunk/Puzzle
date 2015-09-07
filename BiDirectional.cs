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

    public override void Execute(){
      watch = new Stopwatch();
      Console.WriteLine("starting execution for Bi-directional");
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


	var fChildren = BuildChildren(front.GetIndexOfHole());
	var bChildren = BuildChildren(back.GetIndexOfHole());

	foreach(var child in fChildren){

	  if(FrontClosedSet.ContainsKey(child.Key)) continue;

	  FrontOpenSet.Enqueue(child);
	  

	}

	foreach(var child in bChildren){
	  if(BackClosedSet.ContainsKey(child.Key)) continue;

	  BackOpenSet.Enqueue(child);
	}


	//chech if either is in the closed set of each direction
	if(BackClosedSet.ContainsKey(front.Key) || FrontClosedSet.ContainsKey(back.Key)){


	  Console.WriteLine("Found Solution");
	  //found solution
	  break;

	} 


	if(BackOpenSet.Count == 0 && FrontOpenSet.Count == 0){
	  throw new Exception("no solution");
	}

      }
      watch.Stop();
    }

    public List<State> BuildChildren(int index)
    {
      var rv = new List<State>();
      switch (index)
      {
	case 0: // if index is 0, we can move down or left from the perspective of the hole.
	  /*
	     x,1,2
	     3,4,5
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(0,3)));
	  rv.Add(new State(CurrentState.Swap(0,1)));
	  break;
	case 1: // if index is 1, three different positions are available, left, right, and down.
	  /*
	     1,x,2
	     3,4,5
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(0,1))); //move the hole left
	  rv.Add(new State(CurrentState.Swap(1,2))); // move the hole right 
	  rv.Add(new State(CurrentState.Swap(1,4))); // move the hole down
	  break;
	case 2: // if index is 2, the hole is in the right corner, two moves available.
	  /*
	     1,2,x
	     3,4,5
	     6,7,8
	     */

	  rv.Add(new State(CurrentState.Swap(2,1))); //move the hole left
	  rv.Add(new State(CurrentState.Swap(2,5))); // move the hole right 
	  break;

	case 3: //if index is 3, the hole is in the second row, leftmost position.
	  /*
	     1,2,3
	     x,4,5
	     6,7,8
	     */

	  rv.Add(new State(CurrentState.Swap(3,0)));//move hole up
	  rv.Add(new State(CurrentState.Swap(3,4)));//move hole right
	  rv.Add(new State(CurrentState.Swap(3,6)));//move hole down
	  break;

	case 4:
	  /* 1,2,3
	     4,x,5
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(4,1)));//move hole up
	  rv.Add(new State(CurrentState.Swap(4,3)));//move hole left
	  rv.Add(new State(CurrentState.Swap(4,5)));//move hole right
	  rv.Add(new State(CurrentState.Swap(4,7)));//move hole down

	  break;
	case 5:
	  /* 1,2,3
	     4,5,x
	     6,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(5,2)));//move hole up
	  rv.Add(new State(CurrentState.Swap(5,4)));//move hole left
	  rv.Add(new State(CurrentState.Swap(5,8)));//move hole down
	  break;
	case 6:
	  /* 1,2,3
	     4,5,6
	     x,7,8
	     */
	  rv.Add(new State(CurrentState.Swap(6,3)));//move hole up
	  rv.Add(new State(CurrentState.Swap(6,7)));//move hole right
	  break;
	case 7:
	  /* 1,2,3
	     4,5,6
	     7,x,8
	     */
	  rv.Add(new State(CurrentState.Swap(7,4)));//move hole up
	  rv.Add(new State(CurrentState.Swap(7,6)));//move hole left
	  rv.Add(new State(CurrentState.Swap(7,8)));//move hole right
	  break;
	case 8:
	  /* 1,2,3
	     4,5,6
	     7,8,x
	     */
	  rv.Add(new State(CurrentState.Swap(8,5)));//move hole up
	  rv.Add(new State(CurrentState.Swap(8,7)));//move hole left
	  break;
	default:
	  throw new Exception(string.Format("index was out of range {0}", index));
      }
      return rv;
    }
  }
}
