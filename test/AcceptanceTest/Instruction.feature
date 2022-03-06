Feature: Instruction

    Background: Create Plateau and Rover
        Given Plateau size is 5 5
        
    Scenario: Execute given instruction
        Given Rover's coordinate is <coordinateInput> and instruction list is <instructionList>
        When i execute all the instructions
        Then i should see the rover at <coordinateOutput>
    Examples: 
      | coordinateInput | instructionList | coordinateOutput |
      | 1 2 N           | LMLMLMLMM       | 1 3 N            |
      | 3 3 E           | MMRMMRMRRM      | 5 1 E            |