using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib
{
    public enum SalaryType
    {
        AcaDegTitle,
        ProfessorCoefficients
    }

    public enum StudyUnitCoefficientType
    {
        Default,
        TeachingForeignLanguages
    }
    public class SystemConfig
    {
        private static string _user;
        private static SalaryType _currentSalaryType;
        private static StudyUnitCoefficientType _currentStudyUnitCoefficientType;

        public static StudyUnitCoefficientType CurrentStudyUnitCoefficientType
        {
            get { return SystemConfig._currentStudyUnitCoefficientType; }
            set { SystemConfig._currentStudyUnitCoefficientType = value; }
        }

        public static SalaryType CurrentSalaryType
        {
            get { return SystemConfig._currentSalaryType; }
            set { SystemConfig._currentSalaryType = value; }
        }

        public static string User
        {
            get { return SystemConfig._user; }
            set { SystemConfig._user = value; }
        }



        public static void InitSystemConfig()
        {
            _currentSalaryType = SalaryType.ProfessorCoefficients;
            _currentStudyUnitCoefficientType = StudyUnitCoefficientType.TeachingForeignLanguages;
        }
    }
}
