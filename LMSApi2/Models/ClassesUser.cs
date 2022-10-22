namespace LMSApi2.Models
{
    public class ClassesUser
    {
        public int ClassesClassId { get; set; }
        public virtual Classes Classes{get;set;}

        public string UsersUserId { get; set; }
        public virtual User User { get; set; }  

    }
}
