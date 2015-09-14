namespace a1 {
  public class PriorityQueueState : Priority_Queue.PriorityQueueNode {
    public State NodeState { get; private set;}
    public PriorityQueueState(State state){
      NodeState = state;
    }
  }
}
