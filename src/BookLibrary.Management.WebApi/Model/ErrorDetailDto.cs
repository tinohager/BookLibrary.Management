namespace BookLibrary.Management.WebApi.Model
{
    public class ErrorDetailDto
    {
        public string Description { get; set; }

        public ErrorDetailDto(string description)
        {
            this.Description = description;
        }
    }
}
