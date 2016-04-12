using System.Activities;
using System.Security;

namespace UsingPersistenceParticipant
{
    public class CollectDataActivity : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            context.GetExtension<MyPersistenceParticipant>().Message = "hello persistence participant";
        }
    }
}