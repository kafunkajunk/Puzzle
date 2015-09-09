#define DEBUG
using System.Diagnostics;
using System.Collections.Generic;
using System;
namespace a1
{
    public class IterativeDepth : Algorithm
    {
        public Stack<State> OpenSet = new Stack<State>();
        public Dictionary<string, State> ClosedSet = new Dictionary<string, State>();
        public State found = null;

	public IterativeDepth(int[] arr){
	  CurrentState = new State(arr);
	}

        public override void Execute()
        {
            watch = new Stopwatch();
            watch.Start();

            if (!IDDFS(CurrentState))
            {
                Console.WriteLine("Depth reached. No solution found.");
                System.Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);
            }
            else
            {
                Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);
            }
            
        }

        //Helper function for recursive iterative deepening
        public bool IDDFS(State root)
        {
            for (int depth = 0; depth < 31; depth++)
            {
                found = recursive_depth(root, depth);
                if (found != null)
                    return true;
            }
            return false;
        }

        public State recursive_depth(State state, int depth)
        {
            if (depth == 0 && state.IsEqualToGoal())
            {
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
                        if (ClosedSet.ContainsKey(item.Key))
                        {
#if DEBUG
                            Console.WriteLine("ClosedSet already contains key {0}", item.Key);
#endif                      
                            continue;
                        }
                        children.Add(item);
                        OpenSet.Push(item);
                    }

                    state = OpenSet.Pop();
                    moves.AddLast(state);
                    //Debugger.Break();
                    if (OpenSet.Count == 0)
                    {
                        Console.WriteLine("Elapsed time: {0} ms", watch.Elapsed.Milliseconds);
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
    }
}


