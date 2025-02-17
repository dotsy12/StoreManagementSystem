using System.Reflection.PortableExecutable;
using System.Text;
namespace ContactPro
{
    public class DataBaseMangment
    {
        public static void SaveData<T>(List<T> DataList, string PathName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(PathName))
                {
                    foreach (var item in DataList)
                    {
                        string line = string.Join("|", item.GetType().GetProperties().Select(p => p.GetValue(item, null)));
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to {PathName}: {ex.Message}");
            }
        }
        
        
        public static List<T> LoadData<T>(string PathName) where T : new()
        {
            List<T> DataList = new List<T>();
            if (!File.Exists(PathName))
            {
                Console.WriteLine($"File {PathName} does not exist. Creating a new file.");
                return DataList;
            }
            using (StreamReader readData = new StreamReader(PathName))
            {
                string lines;
                while ((lines = readData.ReadLine()) != null)
                {
                    string[] parts = lines.Split('|');
                    T obj = new T();
                    var properties = obj.GetType().GetProperties();

                    for (int i = 0; i < properties.Length; i++)
                    {
                        if (i >= parts.Length) break;

                        try
                        {
                            var propertyType = properties[i].PropertyType;
                            var converter = System.ComponentModel.TypeDescriptor.GetConverter(propertyType);
                            object value = converter.ConvertFromString(parts[i]);
                            properties[i].SetValue(obj, value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error converting data for property {properties[i].Name}: {ex.Message}");
                            // يمكنك تحديد قيمة افتراضية أو تجاهل الخطأ
                        }
                    }

                    DataList.Add(obj);
                }
            }

            return DataList;
        }
    }
}