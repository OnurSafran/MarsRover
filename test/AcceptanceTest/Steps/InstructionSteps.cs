using System.Diagnostics.Contracts;
using MarsRover;
using MarsRover.Domain.Entities;
using TechTalk.SpecFlow;
using Xunit;

namespace AcceptanceTest.Steps
{
    [Binding]
    public class InstructionSteps
    {
        private Controller _controller;
        private RoverSquad _roverSquad;
        private Plateau _plateau;
        
        public InstructionSteps()
        {
        }
        
        [Given(@"Plateau size is (.*)")]
        public void GivenPlateauSizeIs(string p0)
        {
            _plateau = Controller.GetPlateau(p0);
            _roverSquad = new RoverSquad(_plateau);
        }

        [Given(@"Rover's coordinate is (.*) and instruction list is (.*)")]
        public void GivenRoversCoordinateIsAndInstructionListIs(string coordinateInput, string instructionList)
        {
            _roverSquad.DeployRover(Controller.GetCoordinate(coordinateInput), Controller.GetDirection(coordinateInput), Controller
                .GetInstruction(instructionList));
        }

        [When(@"i execute all the instructions")]
        public void WhenIExecuteAllTheInstructions()
        {
            var currentRover = _roverSquad.RoverList[^1];
            
            currentRover.ExecuteInstructions();
        }

        [Then(@"i should see the rover at (.*)")]
        public void ThenIShouldSeeTheRoverAt(string coordinateOutput)
        {
            var currentRover = _roverSquad.RoverList[^1];
            var exceptedCoordinate = Controller.GetCoordinate(coordinateOutput);
            var exceptedDirection = Controller.GetDirection(coordinateOutput);

            Assert.Equal(currentRover.Coordinate, exceptedCoordinate);
            Assert.Equal(currentRover.Direction, exceptedDirection);
        }
    }
}