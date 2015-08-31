using System.Diagnostics;
using System.Collections.Generic;
namespace a1{
  
  ///<summary>
  ///base class for algorithms
  ///</summary>
  public abstract class Algorithm{
    public Stopwatch watch;
    public State CurrentState;
    public LinkedList<State> moves = new LinkedList<State>();
    public abstract void Execute();

  }

}
