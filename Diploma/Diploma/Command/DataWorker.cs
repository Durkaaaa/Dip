using Diploma.Model;
using Diploma.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Command
{
    public class DataWorker
    {
        #region[Талоны]
        //Добавление
        public static string AddNewTicket(Patient selectedPatient, Doctor selectedDoctor,
            DateTime date, ReceptionHour selectedReceptionHour)
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Данная запись уже существует";
                bool examination = db.Ticket.Any(p => p.PatientId == selectedPatient.Id &&
                    p.DoctorId == selectedDoctor.Id && p.Date == date && p.ReceptionHour == selectedReceptionHour);
                if (!examination)
                {
                    Ticket ticket = new Ticket
                    {
                        PatientId = selectedPatient.Id,
                        DoctorId = selectedDoctor.Id,
                        Date = date,
                        ReceptionHourId = selectedReceptionHour.Id
                    };
                    db.Ticket.Add(ticket);
                    db.SaveChanges();
                    result = "Запись добавлена";
                }
                return result;
            }
        }

        //Редактирование
        public static string EditTicket(Ticket selectedTicket, Patient selectedPatient, Doctor selectedDoctor,
            DateTime date, ReceptionHour selectedReceptionHour)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Данная запись уже существует";
                bool examination = db.Ticket.Any(p => p.PatientId == selectedPatient.Id &&
                    p.DoctorId == selectedDoctor.Id && p.Date == date && p.ReceptionHour == selectedReceptionHour);
                if (!examination)
                {
                    var ticket = db.Ticket.FirstOrDefault(p => p.Id == selectedTicket.Id);
                    ticket.PatientId = selectedPatient.Id;
                    ticket.DoctorId = selectedDoctor.Id;
                    ticket.Date = date;
                    ticket.ReceptionHour = selectedReceptionHour;
                    db.SaveChanges();
                    result = "Запись изменена";
                }
                return result;
            }
        }

        //Удаление
        public static string DeleteTicket(Ticket selectedTicket)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var ticket = db.Ticket.FirstOrDefault(p => p.Id == selectedTicket.Id);
                db.Ticket.Remove(ticket);
                db.SaveChanges();
                var result = "Талон удален";
                return result;
            }
        }

        public static List<Speciality> GetAllSpecialityTicket()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var speciality = db.Speciality.ToList();
                for (int i = 0; i < speciality.Count(); i++)
                {
                    var selectedSpeciality = speciality[i];
                    bool examination = db.Doctors.Any(p => p.SpecialityId == selectedSpeciality.Id);
                    if (!examination)
                    {
                        speciality.Remove(selectedSpeciality);
                    }
                }
                return speciality;
            }
        }

        public static List<Patient> GetPatientForTicket(DateTime date, ReceptionHour selectedReceptionHour)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var ticket = db.Ticket.Where(p => p.Date == date && p.ReceptionHourId == selectedReceptionHour.Id).ToList();
                var patient = db.Patients.ToList();
                if (patient.Count() > 0 &&
                    ticket.Count() > 0)
                {
                    for (int i = 0; i < patient.Count(); i++)
                    {
                        var selectedPatient = patient[i];
                        bool exemination = ticket.Any(p => p.PatientId == selectedPatient.Id);
                        if (exemination)
                        {
                            patient.Remove(selectedPatient);
                        }
                    }
                }
                return patient;
            }
        }

        public static List<Doctor> GetDoctorForTicket(DateTime date, ReceptionHour selectedReceptionHour, Speciality selectedSpeciality)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var ticket = db.Ticket.Where(p => p.Date == date && p.ReceptionHourId == selectedReceptionHour.Id).ToList();
                var doctor = db.Doctors.ToList();
                if (doctor.Count() > 0 &&
                    ticket.Count() > 0)
                {
                    for (int i = 0; i < doctor.Count(); i++)
                    {
                        var selectedDoctor = doctor[i];
                        bool exemination = ticket.Any(p => p.DoctorId == selectedDoctor.Id);
                        if (exemination)
                        {
                            doctor.Remove(selectedDoctor);
                        }
                    }
                }

                var start = db.ReceptionHours.FirstOrDefault(p => p.Id == selectedReceptionHour.Id).StartOfReception;
                var end = db.ReceptionHours.FirstOrDefault(p => p.Id == selectedReceptionHour.Id).EndOfReception;
                var result = doctor.Where(p => p.WorkWith.Hour <= start.Hour && p.WorkUntil.Hour >= end.Hour &&
                    p.SpecialityId == selectedSpeciality.Id).ToList();
                return result;
            }
        }

        public static List<Patient> AddPatient(Ticket ticket, List<Patient> patientList)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var patient = db.Patients.FirstOrDefault(p => p.Id == ticket.PatientId);
                patientList.Add(patient);
                return patientList;
            }
        }

        public static List<Doctor> AddDoctor(Ticket ticket, List<Doctor> doctorList)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var docrot = db.Doctors.FirstOrDefault(p => p.Id == ticket.DoctorId);
                doctorList.Add(docrot);
                return doctorList;
            }
        }
        #endregion

        #region[Bool]
        public static bool BoolGetAllMedicineByMedicanRecordId(MedicalRecord medicalRecord)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var medicine = db.Medicines.Any(p => p.MedicalRecordId == medicalRecord.Id);
                return medicine;
            }
        }

        public static bool BoolGetMedicalRecordByMedicalСardId(MedicalCard selectedMedicalCard)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.MedicalRecords.Any(p => p.MedicalСardId == selectedMedicalCard.Id);
                return result;
            }
        }

        public static bool BoolGetMedicalСardByPatientId(Patient selectedPatient)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var medicalСard = db.MedicalСards.Any(p => p.PatientId == selectedPatient.Id);
                return medicalСard;
            }
        }
        #endregion

        #region[Строка по строке из другой таблицы]
        public static MedicalCard GetMedicalСardByPatientId(Patient selectedPatient)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var medicalСard = db.MedicalСards.FirstOrDefault(p => p.PatientId == selectedPatient.Id);
                return medicalСard;
            }
        }

        public static Patient GetPatientByMedicalRecord(MedicalRecord selectedMedicalRecord)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var medicalRecord = db.MedicalRecords.FirstOrDefault(p => p.Id == selectedMedicalRecord.Id);
                var medicalСardId = medicalRecord.MedicalСardId;
                var patientId = db.MedicalСards.FirstOrDefault(p => p.Id == medicalСardId).PatientId;
                var patient = db.Patients.FirstOrDefault(p => p.Id == patientId);
                return patient;
            }
        }

        public static Doctor GetDoctorByMedicalRecord(MedicalRecord selectedMedicalRecord)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var medicalRecord = db.MedicalRecords.FirstOrDefault(p => p.Id == selectedMedicalRecord.Id);
                var doctorId = medicalRecord.DoctorId;
                var doctor = db.Doctors.FirstOrDefault(p => p.Id == doctorId);
                return doctor;
            }
        }
        #endregion

        #region[Страницы в мед карте]
        public static void AddNewMedicalRecord(MedicalCard selectedMedicalCard,
            Doctor selectedDoctor, string diagnosis, DateTime startOfTreatment,
            DateTime? endOfTreatment)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                MedicalRecord medicalRecord = new MedicalRecord
                {
                    MedicalСardId = selectedMedicalCard.Id,
                    DoctorId = selectedDoctor.Id,
                    Diagnosis = diagnosis,
                    StartOfTreatment = startOfTreatment,
                    EndOfTreatment = endOfTreatment
                };
                db.MedicalRecords.Add(medicalRecord);
                db.SaveChanges();
            }
        }

        public static void EditMedicalRecord(MedicalRecord selectedMedicalRecord, MedicalCard selectedMedicalCard,
            Doctor selectedDoctor, string diagnosis, DateTime startOfTreatment,
            DateTime? endOfTreatment)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                MedicalRecord medicalRecord = db.MedicalRecords.FirstOrDefault(p => p.Id == selectedMedicalRecord.Id);
                medicalRecord.MedicalСardId = selectedMedicalCard.Id;
                medicalRecord.DoctorId = selectedDoctor.Id;
                medicalRecord.Diagnosis = diagnosis;
                medicalRecord.StartOfTreatment = startOfTreatment;
                medicalRecord.EndOfTreatment = endOfTreatment;
                db.SaveChanges();
            }
        }

        public static string DeleteMedicalRecord(MedicalRecord selectedMedicalRecord)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                bool boolMedicine = db.Medicines.Any(p => p.Id == selectedMedicalRecord.Id);
                if (boolMedicine)
                {
                    int countMedicine = db.Medicines.Where(p => p.Id == selectedMedicalRecord.Id).Count();
                    for (int i = 0; i < countMedicine; i++)
                    {
                        var medicine = db.Medicines.FirstOrDefault(p => p.Id == selectedMedicalRecord.Id);
                        if (medicine != null)
                        {
                            db.Medicines.Remove(medicine);
                        }
                    }
                }
                db.MedicalRecords.Remove(selectedMedicalRecord);
                db.SaveChanges();
                var result = "Страница удалена";
                return result;
            }
        }

        public static List<MedicalRecord> GetMedicalRecordByMedicalСardId(MedicalCard selectedMedicalCard)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.MedicalRecords.Where(p => p.MedicalСardId == selectedMedicalCard.Id).ToList();
                return result;
            }
        }
        #endregion

        #region[Строка по Id]
        //Кабинет по Id
        public static Cabinet GetCabinetById(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Cabinets.FirstOrDefault(p => p.Id == Id);
                return result;
            }
        }

        //Время по Id
        public static ReceptionHour GetReceptionHourById(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.ReceptionHours.FirstOrDefault(p => p.Id == Id);
                return result;
            }
        }

        //Медицинская карта по Id
        public static MedicalCard GetMedicalСardById(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.MedicalСards.FirstOrDefault(p => p.Id == Id);
                return result;
            }
        }

        //Пациент по Id
        public static Patient GetPatientById(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Patients.FirstOrDefault(p => p.Id == Id);
                return result;
            }
        }

        //Доктор по Id
        public static Doctor GetDoctorById(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Doctors.FirstOrDefault(p => p.Id == Id);
                return result;
            }
        }

        //Специальность по Id
        public static Speciality GetSpecialityById(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Speciality.FirstOrDefault(p => p.Id == Id);
                return result;
            }
        }

        //Пол по Id
        public static Gender GetGenderById(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Genders.FirstOrDefault(p => p.Id == Id);
                return result;
            }
        }
        #endregion

        #region[Получение полных таблиц]
        public static List<Ticket> GetAllTicket()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Ticket.ToList();
                return result;
            }
        }

        public static List<ReceptionHour> GetAllReceptionHour()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.ReceptionHours.ToList();
                return result;
            }
        }

        //Все пациенты=================================================================
        public static List<Patient> GetAllPatient()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Patients.ToList();
                return result;
            }
        }

        //Все доктора
        public static List<Doctor> GetAllDoctor()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Doctors.ToList();
                return result;
            }
        }

        //Все специальности
        public static List<Speciality> GetAllSpeciality()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Speciality.ToList();
                return result;
            }
        }

        //Все пола=================================================================
        public static List<Gender> GetAllGender()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Genders.ToList();
                return result;
            }
        }
        #endregion

        #region[Пациенты]
        //Добавление=================================================================
        public static string AddNewPatient(string surname, string name, string lastname,
            Gender selectedGender, DateTime dateOfBirth, string policy, string snils,
            string passportSeries, string passportNumber, string address)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Данная запись уже существует";
                bool examination = db.Patients.Any(p => p.Surname == surname && p.Name == name &&
                    p.Lastname == lastname && p.GenderId == selectedGender.Id && p.Policy == policy &&
                    p.Snils == snils && p.PassportSeries == passportSeries && p.PassportNumber == passportNumber &&
                    p.Address == address && p.DateOfBirth == dateOfBirth);

                if (!examination)
                {
                    Patient patient = new Patient
                    {
                        Surname = surname,
                        Name = name,
                        Lastname = lastname,
                        GenderId = selectedGender.Id,
                        DateOfBirth = dateOfBirth,
                        Policy = policy,
                        Snils = snils,
                        PassportSeries = passportSeries,
                        PassportNumber = passportNumber,
                        Address = address
                    };
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    result = "Запись добавлена";
                }
                return result;
            }
        }

        //Редактирование=================================================================
        public static string EditPatient(Patient selectedPatient, string surname, string name,
            string lastname, Gender selectedGender, DateTime dateOfBirth, string policy,
            string snils, string passportSeries, string passportNumber, string address)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Данная запись уже существует";
                bool examination = db.Patients.Any(p => p.Surname == surname && p.Name == name &&
                    p.Lastname == lastname && p.GenderId == selectedGender.Id && p.Policy == policy &&
                    p.Snils == snils && p.PassportSeries == passportSeries && p.PassportNumber == passportNumber &&
                    p.Address == address && p.DateOfBirth == dateOfBirth && p.Id == selectedPatient.Id);

                if (!examination)
                {
                    var patient = db.Patients.FirstOrDefault(p => p.Id == selectedPatient.Id);
                    patient.Surname = surname;
                    patient.Name = name;
                    patient.Lastname = lastname;
                    patient.GenderId = selectedGender.Id;
                    patient.DateOfBirth = dateOfBirth;
                    patient.Policy = policy;
                    patient.Snils = snils;
                    patient.PassportSeries = passportSeries;
                    patient.PassportNumber = passportNumber;
                    patient.Address = address;
                    db.SaveChanges();
                    result = "Запись изменена";
                }
                return result;
            }
        }

        //Удаление
        public static string DeletePatient(Patient selectedPatient)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Ошибка";
                if (selectedPatient != null)
                {
                    var medicalCardBool = db.MedicalСards.Any(p => p.PatientId == selectedPatient.Id);
                    if (medicalCardBool)
                    {
                        var medicalCard = db.MedicalСards.FirstOrDefault(p => p.PatientId == selectedPatient.Id);
                        bool medicalRecordsBool = db.MedicalRecords.Any(p => p.MedicalСardId == medicalCard.Id);
                        if (medicalRecordsBool)
                        {
                            var medicalRecords = db.MedicalRecords.Where(p => p.MedicalСardId == medicalCard.Id).ToList();
                            for (int i = 0; i < medicalRecords.Count(); i++)
                            {
                                var selectedMedicalRecord = medicalRecords[i];
                                var medicines = db.Medicines.Where(p => p.MedicalRecordId == selectedMedicalRecord.Id).ToList();
                                for (int j = 0; j < medicines.Count(); j++)
                                {
                                    var selectedMedicine = medicines[j];
                                    db.Medicines.Remove(selectedMedicine);
                                }
                                db.MedicalRecords.Remove(selectedMedicalRecord);
                            }
                        }
                        db.MedicalСards.Remove(medicalCard);
                    }
                    

                    var tickets = db.Ticket.Where(p => p.PatientId == selectedPatient.Id).ToList();
                    for (int i = 0; i < tickets.Count(); i++)
                    {
                        var selectedTickets = tickets[i];
                        db.Ticket.Remove(selectedTickets);
                    }
                    db.Patients.Remove(selectedPatient);                    
                    db.SaveChanges();
                    result = "Запись удалена";
                }
                return result;
            }
        }
        #endregion

        #region[Врачи]
        //Добавление
        public static string AddNewDoctor(string surname, string name, string lastname,
            Speciality selectedSpeciality, Cabinet selectedCabinet, DateTime dateOfEmployment,
            DateTime workWith, DateTime workUntil)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Кабинет занят";
                bool examinationCabinet = db.Doctors.Any(p => p.CabinetId == selectedCabinet.Id);
                if (!examinationCabinet)
                {
                    result = "Данная запись уже существует";
                    bool examination = db.Doctors.Any(p => p.Surname == surname && p.Name == name &&
                        p.Lastname == lastname && p.SpecialityId == selectedSpeciality.Id &&
                        p.DateOfEmployment == dateOfEmployment && p.WorkWith == workWith &&
                        p.WorkUntil == workUntil);

                    if (!examination)
                    {
                        Doctor doctor = new Doctor
                        {
                            Surname = surname,
                            Name = name,
                            Lastname = lastname,
                            SpecialityId = selectedSpeciality.Id,
                            CabinetId = selectedCabinet.Id,
                            DateOfEmployment = dateOfEmployment,
                            WorkWith = workWith,
                            WorkUntil = workUntil
                        };
                        db.Doctors.Add(doctor);
                        db.SaveChanges();
                        result = "Запись добавлена";
                    }
                }
                return result;
            }
        }

        //Редактирование
        public static string EditDoctor(Doctor selectedDoctor, string surname, string name,
            string lastname, Speciality selectedSpeciality, Cabinet selectedCabinet,
            DateTime dateOfEmployment, DateTime workWith, DateTime workUntil)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Данная запись уже существует";
                bool examination = db.Doctors.Any(p => p.Surname == surname && p.Name == name &&
                    p.Lastname == lastname && p.SpecialityId == selectedSpeciality.Id &&
                    p.DateOfEmployment == dateOfEmployment && p.WorkWith == workWith &&
                    p.WorkUntil == workUntil && p.Id == selectedDoctor.Id);

                if (!examination)
                {
                    var doctor = db.Doctors.FirstOrDefault(p => p.Id == selectedDoctor.Id);
                    if (doctor != null)
                    {
                        doctor.Surname = surname;
                        doctor.Name = name;
                        doctor.Lastname = lastname;
                        doctor.SpecialityId = selectedSpeciality.Id;
                        doctor.CabinetId = selectedCabinet.Id;
                        doctor.DateOfEmployment = dateOfEmployment;
                        doctor.WorkWith = workWith;
                        doctor.WorkUntil = workUntil;
                        db.SaveChanges();
                        result = "Запись изменена";
                    }
                }
                return result;
            }
        }

        //Удаление
        public static string DeleteDoctor(Doctor selectedDoctor)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "Ошибка";
                if (selectedDoctor != null)
                {
                    bool medicalRecordsBool = db.MedicalRecords.Any(p => p.DoctorId == selectedDoctor.Id);
                    if (medicalRecordsBool)
                    {
                        var medicalRecords = db.MedicalRecords.Where(p => p.DoctorId == selectedDoctor.Id).ToList();
                        for (int i = 0; i < medicalRecords.Count(); i++)
                        {
                            var selectedMedicalRecord = medicalRecords[i];
                            var medicines = db.Medicines.Where(p => p.MedicalRecordId == selectedMedicalRecord.Id).ToList();
                            for (int j = 0; j < medicines.Count(); j++)
                            {
                                var selectedMedicine = medicines[j];
                                db.Medicines.Remove(selectedMedicine);
                            }
                            db.MedicalRecords.Remove(selectedMedicalRecord);
                        }
                    }

                    var tickets = db.Ticket.Where(p => p.PatientId == selectedDoctor.Id).ToList();
                    for (int i = 0; i < tickets.Count(); i++)
                    {
                        var selectedTickets = tickets[i];
                        db.Ticket.Remove(selectedTickets);
                    }
                    db.Doctors.Remove(selectedDoctor);
                    db.SaveChanges();
                    result = "Запись удалена";
                }
                return result;
            }
        }

        public static List<Cabinet> GetAllCabinet()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Cabinet> cabinet = db.Cabinets.ToList();
                List<Cabinet> result = db.Cabinets.ToList();
                int allCabinet = db.Cabinets.Count() - 1;
                for (int i = 0; i < allCabinet; i++)
                {
                    var selectedCabinet = cabinet[i];
                    if (selectedCabinet != null)
                    {
                        bool examinationCabinet = db.Doctors.Any(p => p.CabinetId == selectedCabinet.Id);
                        if (examinationCabinet)
                        {
                            result.Remove(selectedCabinet);
                        }
                    }
                }
                return result;
            }
        }

        public static List<Cabinet> GetAllCabinet(Doctor selectedDoctor)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Cabinet> cabinet = db.Cabinets.ToList();
                List<Cabinet> result = db.Cabinets.ToList();
                var doctor = db.Doctors.FirstOrDefault(p => p.Id == selectedDoctor.Id);
                int allCabinet = db.Cabinets.Count() - 1;
                for (int i = 0; i < allCabinet; i++)
                {
                    var selectedCabinet = cabinet[i];
                    if (selectedCabinet != null)
                    {
                        bool examinationCabinet = db.Doctors.Any(p => p.CabinetId == selectedCabinet.Id);
                        if (examinationCabinet && selectedCabinet != doctor.Cabinet)
                        {
                            result.Remove(selectedCabinet);
                        }
                    }
                }
                return result;
            }
        }
        #endregion

        #region[Медицинская карта]
        //Добавление
        public static MedicalCard AddNewMedicalСard(Patient selectedPatient)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                MedicalCard medicalCard = new MedicalCard
                {
                    PatientId = selectedPatient.Id
                };
                db.MedicalСards.Add(medicalCard);
                db.SaveChanges();
                return medicalCard;
            }
        }

        //Редактирование
        public static string EditMedicalСard(MedicalCard selectedMedicalСard,
            Patient selectedPatient)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = "У этого пациента уже есть Мед. карта";
                bool examination = db.MedicalСards.Any(p => p.PatientId == selectedPatient.Id &&
                    p.Id == selectedMedicalСard.Id);

                if (!examination)
                {
                    var medicalСard = db.MedicalСards.FirstOrDefault(p => p.Id == selectedMedicalСard.Id);
                    if (medicalСard != null)
                    {
                        medicalСard.PatientId = selectedPatient.Id;
                        db.SaveChanges();
                        result = "Запись изменена";
                    }
                    else
                    {
                        result = "Ошибка";
                    }
                }
                return result;
            }
        }

        //Удаление
        public static string DeleteMedicalСard(MedicalCard selectedMedicalСard)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var medicalCard = db.MedicalСards.FirstOrDefault(p => p.Id == selectedMedicalСard.Id);
                db.MedicalСards.Remove(medicalCard);
                db.SaveChanges();
                var result = "Запись удалена";
                return result;
            }
        }


        #endregion

        #region[Лекарства]
        //Добавление
        public static void AddNewMedicine(MedicalRecord selectedMedicalRecord, string titl)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                bool examination = db.Medicines.Any(p => p.MedicalRecordId == selectedMedicalRecord.Id && p.Titl == titl);
                if (!examination)
                {
                    Medicine medicine = new Medicine
                    {
                        MedicalRecordId = selectedMedicalRecord.Id,
                        Titl = titl
                    };
                    db.Medicines.Add(medicine);
                    db.SaveChanges();
                }
            }
        }

        //Удаление
        public static void DeleteMedicine(Medicine selectedMedicine)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var medicine = db.Medicines.FirstOrDefault(p => p.Id == selectedMedicine.Id);
                db.Medicines.Remove(medicine);
                db.SaveChanges();
            }
        }

        public static List<Medicine> GetAllMedicinesByMedicalRecordId(MedicalRecord medicalRecord)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Medicines.Where(p => p.MedicalRecordId == medicalRecord.Id).ToList();
                return result;
            }
        }
        #endregion

        #region[Получение Index по Id]
        public static int GetIndexCabinet(int Id, List<Cabinet> cabinetsList)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var row = cabinetsList.FirstOrDefault(p => p.Id == Id);
                int index = cabinetsList.IndexOf(row);
                return index;
            }
        }

        public static int GetIndexReceptionHour(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<ReceptionHour> receptionHour = db.ReceptionHours.ToList();
                var row = db.ReceptionHours.FirstOrDefault(p => p.Id == Id);
                int index = receptionHour.IndexOf(row);
                return index;
            }
        }

        public static int GetIndexSpeciality(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Speciality> speciality = db.Speciality.ToList();
                var row = db.Speciality.FirstOrDefault(p => p.Id == Id);
                int index = speciality.IndexOf(row);
                return index;
            }
        }

        public static int GetIndexSpecialityByDoctor(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var doctor = db.Doctors.FirstOrDefault(p => p.Id == Id);
                List<Speciality> speciality = db.Speciality.ToList();
                var row = db.Speciality.FirstOrDefault(p => p.Id == doctor.SpecialityId);
                int index = speciality.IndexOf(row);
                return index;
            }
        }

        //Получение Index по Id пола=================================================================
        public static int GetIndexGender(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Gender> genders = db.Genders.ToList();
                var row = genders.FirstOrDefault(p => p.Id == Id);
                int index = genders.IndexOf(row);
                return index;
            }
        }

        //Получение Index по Id доктора
        public static int GetIndexDoctor(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Doctor> doctor = db.Doctors.ToList();
                var row = db.Doctors.FirstOrDefault(p => p.Id == Id);
                int index = doctor.IndexOf(row);
                return index;
            }
        }

        public static int GetIndexDoctor(List<Doctor> doctorList, int Id)
        {
            var row = doctorList.FirstOrDefault(p => p.Id == Id);
            int index = doctorList.IndexOf(row);
            return index;
        }

        //Получение Index по Id пациента
        public static int GetIndexPatient(int Id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Patient> patient = db.Patients.ToList();
                var row = db.Patients.FirstOrDefault(p => p.Id == Id);
                int index = patient.IndexOf(row);
                return index;
            }
        }

        public static int GetIndexPatient(List<Patient> patientList, int Id)
        {
            var row = patientList.FirstOrDefault(p => p.Id == Id);
            int index = patientList.IndexOf(row);
            return index;
        }
        #endregion
    }
}