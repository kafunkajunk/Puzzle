using System.Diagnostics;
using System.Collections.Generic;
using System;
namespace a1{
  public class DepthFirstSearch : Algorithm{
      public Stack<State> OpenSet = new Stack<State>();
      public Dictionary<string, State> ClosedSet = new Dictionary<string, State>();

      

public override void Execute(){
    watch = new Stopwatch();
    watch.Start();

    recursive_depth(CurrentState);
}

public void recursive_depth(State state){
    if (ClosedSet.ContainsKey(state.Key) == false)
    {
        ClosedSet.Add(state.Key, state);
    }

    if (state.IsEqualToGoal())
    {
        watch.Stop();

        Console.WriteLine("Move list: ");
        var move = moves.First;
        for (var i = 0; i < 100; i++)
        {
            if (moves.Count > 1)
            {
                Console.WriteLine(move.Value);
                move = move.Next;
            }
        }
        if (moves.Count > 100) Console.WriteLine("More than 100 ...");

        Console.WriteLine("Goal: ");
        new State(GlobalVar.GOAL).Format();
    }
    else
    {
        var index = state.GetIndexOfHole();
#if DEBUG
	    Console.WriteLine("index of hole: {0}", index);
#endif
        var list = BuildChildren(index);
#if DEBUG 
	    Console.WriteLine("Children of node: {0}", CurrentState);
	    foreach(var item in list){

	      item.Format();
	      Console.WriteLine();
	    }

#endif  
        foreach (var item in list)
        {
            if (ClosedSet.ContainsKey(item.Key))
            {
#if DEBUG 
		Console.WriteLine("ClosedSet already contains key {0}", item.Key);
#endif
                continue;
            }
            OpenSet.Push(item);
        }
        state = OpenSet.Pop();
        moves.AddLast(state);
        //Debugger.Break();
        if (OpenSet.Count == 0)
        {
            throw new Exception("no solution");
        }
        recursive_depth(state);
    }

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
