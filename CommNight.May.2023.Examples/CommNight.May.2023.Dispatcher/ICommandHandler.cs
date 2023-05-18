namespace CommNight.May._2023.Domain;

public interface ICommand { }

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}