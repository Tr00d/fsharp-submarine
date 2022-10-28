using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;

namespace PetProject.Application.Tests.Csharp;

public class ProgramProperties
{
    [Property]
    public Property DownIncreasesAim() =>
        Prop.ForAll(
            Arb.From<SubmarineState>(),
            Arb.From<int>(),
            (state, value) => FromState(state)
                .ExecuteCommand($"down {value}")
                .Should()
                .Be(FromState(state with {Aim = state.Aim + value})));

    [Property]
    public Property UpDecreasesAim() =>
        Prop.ForAll(
            Arb.From<SubmarineState>(),
            Arb.From<int>(),
            (state, value) => FromState(state)
                .ExecuteCommand($"up {value}")
                .Should()
                .Be(FromState(state with {Aim = state.Aim - value})));

    [Property]
    public Property ForwardIncreasesPositionAndDepth() =>
        Prop.ForAll(
            Arb.From<SubmarineState>(),
            Arb.From<int>(),
            (state, value) =>
                FromState(state)
                    .ExecuteCommand($"forward {value}")
                    .Should()
                    .Be(FromState(state with
                    {
                        Position = state.Position + value, Depth = state.Depth + value * state.Aim,
                    })));

    private static Program.Submarine FromState(SubmarineState state) => new(state.Aim, state.Position, state.Depth);

    private record SubmarineState(int Aim, int Position, int Depth);
}