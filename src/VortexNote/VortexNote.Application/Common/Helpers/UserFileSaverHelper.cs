using FluentResults;
using Newtonsoft.Json;
using VortexNote.Domain.Base.Files;
using VortexNote.Domain.Statics;

namespace VortexNote.Application.Common.Helpers
{
    public static class UserFileSaverHelper
    {
        public static Result SaveInFile(string path, string filePath, SavedData data)
        {
            try
            {
                Directory.CreateDirectory(path);

                File.WriteAllText(filePath, JsonConvert.SerializeObject(data));

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public static async Task<Result<SavedData>> ReadFromFile(string path, CancellationToken cancellationToken)
        {
            var jsonData = await File.ReadAllTextAsync(path, cancellationToken);
            if (string.IsNullOrEmpty(jsonData))
                return Result.Fail("No saved data");

            try
            {
                var savedData = JsonConvert.DeserializeObject<SavedData>(jsonData);
                if (savedData is null)
                    return Result.Fail("Errored saving data");
                return Result.Ok(savedData);
            }
            catch
            {
                return Result.Fail("Erroring saving data");
            }
        }

        public static Result<Guid> GetUIDInFile(string path)
        {
            try
            {
                var jsonData = File.ReadAllText(path);

                if (!string.IsNullOrEmpty(jsonData))
                {
                    var savedData = JsonConvert.DeserializeObject<SavedData>(jsonData);
                    if(Guid.TryParse(savedData.UserId, out Guid res))
                        return Result.Ok(res);
                }

                return Result.Fail($"Problem with founding or parsing UserId in {ApplicationStatic.APP_FILE_NAME} file");
            }
            catch
            {
                return Result.Fail($"Problem with founding {ApplicationStatic.APP_FILE_NAME} file");
            }
        }
    }
}