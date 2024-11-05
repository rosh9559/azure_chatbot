using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;

public class MainDialog : ComponentDialog
{
    public MainDialog() : base(nameof(MainDialog))
    {
        var waterfallSteps = new WaterfallStep[]
        {
            AskNameStepAsync,
            SayHelloStepAsync
        };

        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
        AddDialog(new TextInputDialog("Name"));
    }

    private async Task<DialogTurnResult> AskNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        // Prompt user for their name
        return await stepContext.PromptAsync("Name", new PromptOptions
        {
            Prompt = MessageFactory.Text("What is your name?")
        }, cancellationToken);
    }

    private async Task<DialogTurnResult> SayHelloStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        // Say hello to the user
        var name = (string)stepContext.Result;
        await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Hello, {name}! Welcome to the Azure Bot."), cancellationToken);
        return await stepContext.EndDialogAsync(cancellationToken);
    }
}
