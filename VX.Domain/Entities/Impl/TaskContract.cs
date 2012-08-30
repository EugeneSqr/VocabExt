using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VX.Domain.Entities.Impl
{
    [DataContract]
    [KnownType(typeof(WordContract))]
    public class TaskContract : ITask
    {
        [DataMember]
        public IWord Question { get; set; }

        [DataMember]
        public IWord CorrectAnswer { get; set; }

        [DataMember]
        public IList<IWord> Answers { get; set; }
    }
}