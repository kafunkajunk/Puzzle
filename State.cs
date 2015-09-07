using System.Linq;
using std = System.Console;
using System;

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

