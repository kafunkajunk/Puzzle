#define DEBUG
using System.Diagnostics;
using System.Collections.Generic;
using System;
namespace a1
{
  public class DepthFirstLimited : Algorithm
  {
    public Stack<State> OpenSet = new Stack<State>();
    public Dictionary<string, State> ClosedSet = new Dictionary<string, State>();
    public int depth = 0;

    public DepthFirstLimited(int[] arr){
      CurrentState = new State(arr);
    }


    public override void Execute()
    {
      watch = new Stopwatch();
      watch.Start();

      recursive_depth(CurrentState);
    }

    public void recursive_depth(State state)
    {
      if (depth < 31)
      {
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
	  var list = state.BuildChildren();
#if DEBUG
	  Console.WriteLine("Children of node: {0}", state);
	  foreach (var item in list)
	  {

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
	  depth++;
	  recursive_depth(state);
	}
	System.Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);
      }
      else
      {
	Console.WriteLine("Depth reached. No solution found.");
	System.Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);
      }
    }
  }
}

