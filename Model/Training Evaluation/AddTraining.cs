namespace hrd_backend.Model.Training_Evaluation
{
    public class AddTraining
    {
       // public string status {  get; set; }

        public string name { get; set; }

        public string designation { get; set; }

        public string courseTitle { get; set; }

        public string trainingInstitution { get; set; }

        public string trainerName { get; set; }

        public string trainingDate {  get; set; }

        public string trainingVenue { get; set; }

        public string trainingNature {  get; set; }

        public string programObjective { get; set; }

        public string programSupport { get; set; }

        public string trainerPresentation { get; set; }

        public string testResult { get; set; }

        public string trainingBenefit { get; set; }

        public string generalComment { get; set; }

        public Guid requesterId { get; set; }

        public string verifierEmpId { get; set; }


    }
}
