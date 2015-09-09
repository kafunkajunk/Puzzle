using System.Diagnostics;
using System.Collections.Generic;
using System;
namespace a1
{
  public class IterativeDepth : Algorithm
  {
    public Stack<State> OpenSet = new Stack<State>();
    public Dictionary<string, State> ClosedSet = new Dictionary<string, State>();
    public Dictionary<string, State> parents = new Dictionary<string, State>();
    public State found = null;

    public IterativeDepth(int[] arr){
      CurrentState = new State(arr);
    }

    public override void Execute()
    {
      watch = new Stopwatch();
      Console.WriteLine("Initial State: " );
      CurrentState.Format();
      watch.Start();

      if (!IDDFS(CurrentState))
      {
	Console.WriteLine("Depth reached. No solution found.");
      }
      Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);

    }

    //Helper function for recursive iterative deepening
    public bool IDDFS(State root)
    {
      for (int depth = 0; depth < 31; depth++)
      {
	found = recursive_depth(root, depth);

	if (found != null) return true;

	//since we are starting the next loop, the sets for open and closed must be cleared
	//otherwise the search will never be able to traverse down deeper.

	ClosedSet.Clear();
	OpenSet.Clear();
	parents.Clear();

      }
      return false;
    }

    public State recursive_depth(State state, int depth)
    {
      if (depth == 0 && state.IsEqualToGoal())
      {

	watch.Stop();

	SolutionFound(state);
	return state;
      }

      if (depth > 0)
      {
	if (ClosedSet.ContainsKey(state.Key) == false)
	{
	  ClosedSet.Add(state.Key, state);
	}

	if (state.IsEqualToGoal())
	{
	  watch.Stop();

	  SolutionFound(state);

	  return state;
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
	  List<State> children = new List<State>();
	  foreach (var item in list)
	  {
	    if (ClosedSet.ContainsKey(item.Key)) //the second depth won't be able to add its children into the open set thus never finding the solution.
	    {
#if DEBUG
	      Console.WriteLine("ClosedSet already contains key {0}", item.Key);
#endif                      
	      continue;
	    }
	    children.Add(item);
	    OpenSet.Push(item);
	    if(!parents.ContainsKey(item.Key)) parents.Add(item.Key,state);

	  }

	  state = OpenSet.Pop();


	  if (OpenSet.Count == 0)
	  {

	    throw new Exception("no solution");
	  }

	  foreach (var child in children)
	  {
	    found = recursive_depth(child, depth--);

	    if (found != null)
	      return found;

	  }
	  return null;
	}
      }
      return null;
    }

    private void SolutionFound(State FinalState){

      var temp = FinalState;

      while(true){

	if(!parents.ContainsKey(temp.Key)) break;

	moves.AddFirst(parents[temp.Key]);
	temp = parents[temp.Key];
      }

      Console.WriteLine("Move list: ");
      var move = moves.First;
      int count = 0;
      while(move != null) {

	Console.WriteLine( move.Value );
	move = move.Next;

	if(count++ > 100) break;

      }
      if(moves.Count > 100) Console.WriteLine("More than 100 ...");
      Console.WriteLine("Goal: ");
      new State(GlobalVar.GOAL).Format();
    }
  }
}



