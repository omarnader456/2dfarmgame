using UnityEngine;

public class itemtoolbarpanel : itempanel
{
    [SerializeField] private toolbarcontroller toolbarcontroller;


    private void Start()
    {
        init();
        toolbarcontroller.onchange += highlight;
        highlight(0);
    }
    public override void onclick(int id)
    {
        toolbarcontroller.set(id);
        highlight(id);
    }

    private int currentselectedtool;
    public void highlight(int id)
    {
        buttons[currentselectedtool].highlighter(false);
        currentselectedtool = id;
        buttons[currentselectedtool].highlighter(true);
    }
   
    public override void show()
    {
        base.show();
        toolbarcontroller.updatehighlighticon();
    }
}
