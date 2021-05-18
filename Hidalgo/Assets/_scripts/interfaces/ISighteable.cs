using System.Collections;

public interface ISighteable 
{
    void GetSeen(FieldOfView seenBy);
    IEnumerator CheckStillInView(FieldOfView view);

}
