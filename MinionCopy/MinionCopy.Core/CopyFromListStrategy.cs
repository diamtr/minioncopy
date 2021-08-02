using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MinionCopy
{
  public class CopyFromListStrategy : CopyManyStrategy
  {
    public override void Copy()
    {
      this
        .ValidateReqiredProperties()
        .MakeSourcePathRooted()
        .WithSourceExistsValidation();
      this.ReadSource();

      foreach (var item in this.GetChildren())
        item.Copy();
    }

    public override ICopyStrategy ValidateReqiredProperties()
    {
      if (string.IsNullOrWhiteSpace(this.Source))
        throw new ArgumentException($"{nameof(CopyFromListStrategy)}. '{nameof(this.Source)}' is null or empty.");

      return this;
    }
    public override ICopyStrategy WithSourceExistsValidation()
    {
      if(!(new FileInfo(this.Source).Exists))
        throw new ArgumentException($"{nameof(CopyFromListStrategy)}. File in '{nameof(this.Source)}' does not exist.");

      return this;
    }

    public void ReadSource()
    {
      var content = File.ReadAllText(this.Source);
      var settings = CopyStrategy.Json.GetDefaultSerializerSettings();
      this.Items = JsonConvert.DeserializeObject<CopyFromListStrategy>(content, settings).Items;
    }
  }
}
