using System.Activities;

namespace UsingCustomizedExtension
{
    public class UseSimpleExtension : NativeActivity
    {
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            metadata.AddDefaultExtensionProvider<SimpleExtension>(() => new SimpleExtension());
        }

        protected override void Execute(NativeActivityContext context)
        {
            var extension = context.GetExtension<SimpleExtension>();
            extension.DoSomething();
        }
    }
}