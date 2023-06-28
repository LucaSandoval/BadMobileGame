//Any shape should have the following properties. 
//The abstract Shape class will define their base functionality
//(Which I can't imagine will be much different between shapes but
//you never know idk)
public interface IShape 
{
    //Muliply this shape by a factor of whatever. Returns an array with references to the new shapes. 
    IShape[] MuliplyShape(int factor);

    //Destroys this shape. Children can define how that looks visually. 
    void DestroyShape();
}
