using Xunit;

namespace MailerFunctions.Vertical.Tests.Features.Member;

public class Triggers
{
    [Fact]
    public async Task Should_Trigger_XYZ()
    { }
}

public class Process
{
    public class CommandHandler
    {
        [Fact]
        public async Task Should_Emit_XYZ()
        { }
    }

    public class CommandValidator
    {
        [Fact]
        public async Task Should_ValidateOK()
        { }
    }
}