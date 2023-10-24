using Microsoft.AspNetCore.Mvc;

namespace ComboArrayGenerator.Controllers.ArrayGenerators
{
    [Route("StringGenerator")]
    [ApiController]
    public class StringGenerator : ControllerBase
    {
        // Returns the uniqueStrings array.
        [HttpGet("fixed-size")]
        public IActionResult GetFixedSizeStrings()
        {
            char characterRangeStart = 'a';
            char characterRangeEnd = 'z';
            int desiredStringCount = 1000;
            string[] uniqueStrings = new string[desiredStringCount];
            int currentIndex = 0;

            for (char firstChar = characterRangeStart; firstChar <= characterRangeEnd; firstChar++)
            {
                for (
                    char secondChar = characterRangeStart;
                    secondChar <= characterRangeEnd;
                    secondChar++
                )
                {
                    for (
                        char thirdChar = characterRangeStart;
                        thirdChar <= characterRangeEnd;
                        thirdChar++
                    )
                    {
                        if (currentIndex < desiredStringCount)
                        {
                            uniqueStrings[currentIndex] = $"{firstChar}{secondChar}{thirdChar}";
                            currentIndex++;
                        }
                    }
                }
            }

            return Ok(uniqueStrings);
        }

        /* Generate unique strings with the specified count */
        [HttpGet("dynamic-size")]
        public IActionResult GetDynamicSizeStrings(int count)
        {
            if (count < 1 || count > 10000)
            {
                return BadRequest(
                    "Invalid count value. Please specify a count between 1 and 10,000."
                );
            }

            char characterRangeStart = 'a';
            char characterRangeEnd = 'z';
            List<string> uniqueStrings = new List<string>();

            for (char firstChar = characterRangeStart; firstChar <= characterRangeEnd; firstChar++)
            {
                for (
                    char secondChar = characterRangeStart;
                    secondChar <= characterRangeEnd;
                    secondChar++
                )
                {
                    for (
                        char thirdChar = characterRangeStart;
                        thirdChar <= characterRangeEnd;
                        thirdChar++
                    )
                    {
                        if (uniqueStrings.Count < count)
                        {
                            uniqueStrings.Add($"{firstChar}{secondChar}{thirdChar}");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return Ok(uniqueStrings);
        }

        // Generate unique strings for the specified page and pageSize.
        [HttpGet("paged")]
        public IActionResult GetPagedStrings(int page, int pageSize)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest("Invalid page or pageSize values. Please specify valid values.");
            }

            int totalUniqueStrings = 17576; // Total number of unique strings in the character range 'a' to 'z' for 3 characters.

            // Calculate the starting and ending indices for the requested page and pageSize.
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalUniqueStrings - 1);

            // check if requested page is within the limit.
            if (startIndex >= totalUniqueStrings)
            {
                return NotFound("No more unique strings to display.");
            }

            List<string> pagedUniqueStrings = new List<string>();

            for (int i = startIndex; i <= endIndex; i++)
            {
                pagedUniqueStrings.Add(GenerateUniqueStringAtIndex(i));
            }

            return Ok(pagedUniqueStrings);
        }

        // Helper method to generate a unique string at a specific index.
        private string GenerateUniqueStringAtIndex(int index)
        {
            char characterRangeStart = 'a';
            int charactersInAlphabet = 26; // There are 26 characters in English.

            char firstChar = (char)(
                characterRangeStart + (index / (charactersInAlphabet * charactersInAlphabet))
            );
            char secondChar = (char)(
                characterRangeStart + ((index / charactersInAlphabet) % charactersInAlphabet)
            );
            char thirdChar = (char)(characterRangeStart + (index % charactersInAlphabet));

            return $"{firstChar}{secondChar}{thirdChar}";
        }
    }
}
