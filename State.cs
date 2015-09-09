using System.Linq;
using std = System.Console;
using System;
using System.Collections.Generic;

namespace a1{

  public class State{
    public int[] StateArray = new int[9];
    private string _Key = "";

      public string Key {
	get{
	  if(string.IsNullOrWhiteSpace(_Key)){
	    _Key = string.Join("",StateArray);
	  }
	    return _Key;
	}
	set{
	  _Key = value;
	}
      }

    public State(int[] input){
      StateArray = input;
    }
    public void Format(){
      for(int i = 0; i < 9; i++){

	std.Write(StateArray[i]);
	if((i + 1) % 3 == 0 ) std.WriteLine();

      }
    }

    public int GetIndexOfHole(){
      return Array.FindIndex(StateArray, s => s == 0);
    }

    public List<State> BuildChildren(){

      int index = GetIndexOfHole();

      var rv = new List<State>();
      /* so we're going to get all of the children nodes from the current node
	 each one of these is a state that the current state transition to. The number of children change based on the current index of the hole.
	 */
      switch(index){

	case 0: // if index is 0, we can move down or left from the perspective of the hole.
	  /*
	     x,1,2
	     3,4,5
	     6,7,8
	     */
	  rv.Add(new State(Swap(0,3)));
	  rv.Add(new State(Swap(0,1)));
	  break;
	case 1: // if index is 1, three different positions are available, left, right, and down.
	  /*
	     1,x,2
	     3,4,5
	     6,7,8
	     */
	  rv.Add(new State(Swap(0,1))); //move the hole left
	  rv.Add(new State(Swap(1,2))); // move the hole right 
	  rv.Add(new State(Swap(1,4))); // move the hole down
	  break;
	case 2: // if index is 2, the hole is in the right corner, two moves available.
	  /*
	     1,2,x
	     3,4,5
	     6,7,8
	     */

	  rv.Add(new State(Swap(2,1))); //move the hole left
	  rv.Add(new State(Swap(2,5))); // move the hole right 
	  break;

	case 3: //if index is 3, the hole is in the second row, leftmost position.
	  /*
	     1,2,3
	     x,4,5
	     6,7,8
	     */

	  rv.Add(new State(Swap(3,0)));//move hole up
	  rv.Add(new State(Swap(3,4)));//move hole right
	  rv.Add(new State(Swap(3,6)));//move hole down
	  break;

	case 4:
	  /* 1,2,3
	     4,x,5
	     6,7,8
	     */
	  rv.Add(new State(Swap(4,1)));//move hole up
	  rv.Add(new State(Swap(4,3)));//move hole left
	  rv.Add(new State(Swap(4,5)));//move hole right
	  rv.Add(new State(Swap(4,7)));//move hole down

	  break;
	case 5:
	  /* 1,2,3
	     4,5,x
	     6,7,8
	     */
	  rv.Add(new State(Swap(5,2)));//move hole up
	  rv.Add(new State(Swap(5,4)));//move hole left
	  rv.Add(new State(Swap(5,8)));//move hole down
	  break;
	case 6:
	  /* 1,2,3
	     4,5,6
	     x,7,8
	     */
	  rv.Add(new State(Swap(6,3)));//move hole up
	  rv.Add(new State(Swap(6,7)));//move hole right
	  break;
	case 7:
	  /* 1,2,3
	     4,5,6
	     7,x,8
	     */
	  rv.Add(new State(Swap(7,4)));//move hole up
	  rv.Add(new State(Swap(7,6)));//move hole left
	  rv.Add(new State(Swap(7,8)));//move hole right
	  break;
	case 8:
	  /* 1,2,3
	     4,5,6
	     7,8,x
	     */
	  rv.Add(new State(Swap(8,5)));//move hole up
	  rv.Add(new State(Swap(8,7)));//move hole left
	  break;
	default:
	  throw new Exception(string.Format("index was out of range {0}", index));
      }
      return rv;
    }

    public int[] Swap(int index1, int index2){
      var clone = (int[])StateArray.Clone();
      int item1 = StateArray[index1];
      int item2 = StateArray[index2];
      clone[index1] = item2;
      clone[index2] = item1; 

      return clone;
    }
    public bool IsEqualToGoal(int[] goal = null){



      goal = goal ?? GlobalVar.GOAL;
      return StateArray.SequenceEqual(goal);

    }

    public override string ToString(){
      string s = "";
      foreach(var i in StateArray){
       s += i;
      }
      return s;
    }
  }
}

