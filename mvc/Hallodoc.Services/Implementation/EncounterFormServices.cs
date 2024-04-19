using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HallodocServices.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout.Properties;
using iText.Layout.Element;
using DocumentFormat.OpenXml.Bibliography;
using HalloDoc.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace HallodocServices.Implementation
{
    public class EncounterFormServices : IEncounterFormServices
    {
        private readonly IEncounterRepo _encounterRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IRequestFileRepo _requestFileRepo;
        private readonly IRequestNoteRepo _requestNoteRepo;

        public EncounterFormServices(IEncounterRepo encounterRepo,IRequestRepo requestRepo,IRequestFileRepo requestFileRepo,IRequestNoteRepo requestNoteRepo)
        {
            _encounterRepo = encounterRepo;
            _requestRepo = requestRepo;
            _requestFileRepo = requestFileRepo;
            _requestNoteRepo = requestNoteRepo;
        }

        public AdminEncounterForm GetEncounterFormData(int requestid)
        {
            Encounter encounter = _encounterRepo.GetData(requestid);
            if (encounter != null)
            {
                AdminEncounterForm adminEncounterForm = new AdminEncounterForm();
                adminEncounterForm.FirstName = encounter.FirstName;
                adminEncounterForm.LastName = encounter.LastName;
                adminEncounterForm.PhoneNumber = encounter.PhoneNumber;
                adminEncounterForm.Email = encounter.Email;
                adminEncounterForm.Cv = encounter.Cv;
                adminEncounterForm.Chest = encounter.Chest;
                adminEncounterForm.Extr = encounter.Extr;
                adminEncounterForm.Skin = encounter.Skin;
                adminEncounterForm.Pain = encounter.Pain;
                adminEncounterForm.Neuro = encounter.Neuro;
                adminEncounterForm.Procedures = encounter.Procedures;
                adminEncounterForm.Abd = encounter.Abd;
                adminEncounterForm.Allergies = encounter.Allergies;
                adminEncounterForm.BloodPressure = encounter.BloodPressure;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Followup = encounter.Followup;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Location = encounter.Location;
                adminEncounterForm.HeartRate = encounter.HeartRate;
                adminEncounterForm.Temperature = encounter.Temperature;
                adminEncounterForm.RespiratoryRate = encounter.RespiratoryRate;
                adminEncounterForm.MedicalHistory = encounter.MedicalHistory;
                adminEncounterForm.MedicationsDispensed = encounter.MedicationsDispensed;
                adminEncounterForm.O2 = encounter.O2;
                adminEncounterForm.TreatmentPlan = encounter.TreatmentPlan;
                adminEncounterForm.IllnessOrInjury = encounter.IllnessOrInjury;
                adminEncounterForm.RequestId = requestid;
                adminEncounterForm.EncounterId = encounter.EncounterId;
                


                return adminEncounterForm;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateaddEncounterFormData(AdminEncounterForm encounter)
        {
          
            if (encounter.EncounterId > 0)
            {

               Encounter adminEncounterForm = _encounterRepo.GetData(encounter.RequestId);
                adminEncounterForm.FirstName = encounter.FirstName;
                adminEncounterForm.LastName = encounter.LastName;
                adminEncounterForm.PhoneNumber = encounter.PhoneNumber;
                adminEncounterForm.Email = encounter.Email;
                adminEncounterForm.Cv = encounter.Cv;
                adminEncounterForm.Chest = encounter.Chest;
                adminEncounterForm.Extr = encounter.Extr;
                adminEncounterForm.Skin = encounter.Skin;
                adminEncounterForm.Pain = encounter.Pain;
                adminEncounterForm.Neuro = encounter.Neuro;
                adminEncounterForm.Procedures = encounter.Procedures;
                adminEncounterForm.Abd = encounter.Abd;
                adminEncounterForm.Allergies = encounter.Allergies;
                adminEncounterForm.BloodPressure = encounter.BloodPressure;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Followup = encounter.Followup;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Location = encounter.Location;
                adminEncounterForm.HeartRate = encounter.HeartRate;
                adminEncounterForm.Temperature = encounter.Temperature;
                adminEncounterForm.RespiratoryRate = encounter.RespiratoryRate;
                adminEncounterForm.MedicalHistory = encounter.MedicalHistory;
                adminEncounterForm.MedicationsDispensed = encounter.MedicationsDispensed;
                adminEncounterForm.O2 = encounter.O2;
                adminEncounterForm.TreatmentPlan = encounter.TreatmentPlan;
                adminEncounterForm.IllnessOrInjury = encounter.IllnessOrInjury;

              Encounter encounter1 = await _encounterRepo.UpDateData(adminEncounterForm);



            }
            else
            {
                Encounter adminEncounterForm = new();
                adminEncounterForm.FirstName = encounter.FirstName;
                adminEncounterForm.LastName = encounter.LastName;
                adminEncounterForm.PhoneNumber = encounter.PhoneNumber;
                adminEncounterForm.Email = encounter.Email;
                adminEncounterForm.Cv = encounter.Cv;
                adminEncounterForm.Chest = encounter.Chest;
                adminEncounterForm.Extr = encounter.Extr;
                adminEncounterForm.Skin = encounter.Skin;
                adminEncounterForm.Pain = encounter.Pain;
                adminEncounterForm.Neuro = encounter.Neuro;
                adminEncounterForm.Procedures = encounter.Procedures;
                adminEncounterForm.Abd = encounter.Abd;
                adminEncounterForm.Allergies = encounter.Allergies;
                adminEncounterForm.BloodPressure = encounter.BloodPressure;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Followup = encounter.Followup;
                adminEncounterForm.Diagnosis = encounter.Diagnosis;
                adminEncounterForm.Location = encounter.Location;
                adminEncounterForm.HeartRate = encounter.HeartRate;
                adminEncounterForm.Temperature = encounter.Temperature;
                adminEncounterForm.RespiratoryRate = encounter.RespiratoryRate;
                adminEncounterForm.MedicalHistory = encounter.MedicalHistory;
                adminEncounterForm.MedicationsDispensed = encounter.MedicationsDispensed;
                adminEncounterForm.O2 = encounter.O2;
                adminEncounterForm.TreatmentPlan = encounter.TreatmentPlan;
                adminEncounterForm.IllnessOrInjury = encounter.IllnessOrInjury;
                adminEncounterForm.RequestId = encounter.RequestId;

                Encounter encounter1 = await _encounterRepo.AddData(adminEncounterForm);


            }

            return true;
        }

        public async Task<bool> HouseCall(int RequestId)
        {
            Request request = _requestRepo.GetRequest(RequestId);
            request.CallType = 1;
            await _requestRepo.UpdateTable(request);
            return true;
        }

        public byte[] GeneratePDFServices(AdminEncounterForm encounterDetails)
        {
            // Create a MemoryStream to store the PDF
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream)

;
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                Div div = new Div();

                document.Add(new Paragraph("Medical Report")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20));


                document.Add(new Paragraph($"Patient Name: \t\t {encounterDetails.FirstName + " " + encounterDetails.LastName}"));
                document.Add(new Paragraph($"Date Of Birth: \t\t {encounterDetails.BirthDate}"));
                document.Add(new Paragraph($"Report Date:\t\t {encounterDetails.Date}"));
                document.Add(new Paragraph($"PDF Generate Date:\t\t {DateTime.Now.ToShortDateString()}"));
                document.Add(new Paragraph($"Address:\t\t {encounterDetails.Location}"));
                div.Add(new Paragraph($"Mobile Number:\t\t {encounterDetails.PhoneNumber}"));
                div.Add(new Paragraph($"Email:\t\t {encounterDetails.Email}"));


                Table mainTable = new Table(UnitValue.CreatePercentArray(new float[] { 500 }));
                Table nestedTable1 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable2 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable3 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable4 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable5 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable6 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable7 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable8 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable9 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable10 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable11 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable12 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable13 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable14 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable15 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable16 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable17 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable18 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable19 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable20 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable21 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable22 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable23 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));
                Table nestedTable24 = new Table(UnitValue.CreatePercentArray(new float[] { 100, 395 }));

                mainTable.SetWidth(500);
                nestedTable1.SetMinWidth(495);
                nestedTable2.SetMinWidth(495);
                nestedTable3.SetMinWidth(495);
                nestedTable4.SetMinWidth(495);
                nestedTable5.SetMinWidth(495);
                nestedTable6.SetMinWidth(495);
                nestedTable7.SetMinWidth(495);
                nestedTable8.SetMinWidth(495);
                nestedTable9.SetMinWidth(495);
                nestedTable10.SetMinWidth(495);
                nestedTable11.SetMinWidth(495);
                nestedTable12.SetMinWidth(495);
                nestedTable13.SetMinWidth(495);
                nestedTable14.SetMinWidth(495);
                nestedTable15.SetMinWidth(495);
                nestedTable16.SetMinWidth(495);
                nestedTable17.SetMinWidth(495);
                nestedTable18.SetMinWidth(495);
                nestedTable19.SetMinWidth(495);
                nestedTable20.SetMinWidth(495);
                nestedTable21.SetMinWidth(495);
                nestedTable22.SetMinWidth(495);
                nestedTable23.SetMinWidth(495);
                nestedTable24.SetMinWidth(495);


                nestedTable1.AddCell(new Cell().Add(new Paragraph("History of Illness:").SetBold()).SetWidth(100));
                nestedTable1.AddCell(new Cell().Add(new Paragraph(encounterDetails.MedicalHistory ?? "-")));

                nestedTable2.AddCell(new Cell().Add(new Paragraph("Medial History:").SetBold()).SetWidth(100));
                nestedTable2.AddCell(new Cell().Add(new Paragraph(encounterDetails.MedicalHistory ?? "-")));

                nestedTable3.AddCell(new Cell().Add(new Paragraph("Medications:").SetBold()).SetWidth(100));
                nestedTable3.AddCell(new Cell().Add(new Paragraph(encounterDetails.Medications ?? "-")));

                nestedTable4.AddCell(new Cell().Add(new Paragraph("Allergies:").SetBold()).SetWidth(100));
                nestedTable4.AddCell(new Cell().Add(new Paragraph(encounterDetails.Allergies ?? "-")));

                nestedTable5.AddCell(new Cell().Add(new Paragraph("Temp:").SetBold()).SetWidth(100));
                nestedTable5.AddCell(new Cell().Add(new Paragraph(encounterDetails.Temperature ?? "-")));

                nestedTable6.AddCell(new Cell().Add(new Paragraph("HR:").SetBold()).SetWidth(100));
                nestedTable6.AddCell(new Cell().Add(new Paragraph(encounterDetails.HeartRate ?? "-")));

                nestedTable7.AddCell(new Cell().Add(new Paragraph("RR:").SetBold()).SetWidth(100));
                nestedTable7.AddCell(new Cell().Add(new Paragraph(encounterDetails.RespiratoryRate ?? "-")));

                nestedTable8.AddCell(new Cell().Add(new Paragraph("Blood Pressure Systolic:").SetBold()).SetWidth(100));
                nestedTable8.AddCell(new Cell().Add(new Paragraph(encounterDetails.BloodPressure ?? "-")));

                nestedTable9.AddCell(new Cell().Add(new Paragraph("Blood Pressure Diastolic:").SetBold()).SetWidth(100));
                nestedTable9.AddCell(new Cell().Add(new Paragraph(encounterDetails.BloodPressure ?? "-")));

                nestedTable10.AddCell(new Cell().Add(new Paragraph("O2:").SetBold()).SetWidth(100));
                nestedTable10.AddCell(new Cell().Add(new Paragraph(encounterDetails.O2 ?? "-")));

                nestedTable11.AddCell(new Cell().Add(new Paragraph("Pain:").SetBold()).SetWidth(100));
                nestedTable11.AddCell(new Cell().Add(new Paragraph(encounterDetails.Pain ?? "-")));

                nestedTable12.AddCell(new Cell().Add(new Paragraph("Heent:").SetBold()).SetWidth(100));
                nestedTable12.AddCell(new Cell().Add(new Paragraph(encounterDetails.Heent ?? "-")));

                nestedTable13.AddCell(new Cell().Add(new Paragraph("CV:").SetBold()).SetWidth(100));
                nestedTable13.AddCell(new Cell().Add(new Paragraph(encounterDetails.Cv ?? "-")));

                nestedTable14.AddCell(new Cell().Add(new Paragraph("Chest:").SetBold()).SetWidth(100));
                nestedTable14.AddCell(new Cell().Add(new Paragraph(encounterDetails.Chest ?? "-")));

                nestedTable15.AddCell(new Cell().Add(new Paragraph("Abd:").SetBold()).SetWidth(100));
                nestedTable15.AddCell(new Cell().Add(new Paragraph(encounterDetails.Abd ?? "-")));

                nestedTable16.AddCell(new Cell().Add(new Paragraph("Extr:").SetBold()).SetWidth(100));
                nestedTable16.AddCell(new Cell().Add(new Paragraph(encounterDetails.Extr ?? "-")));

                nestedTable17.AddCell(new Cell().Add(new Paragraph("Skin:").SetBold()).SetWidth(100));
                nestedTable17.AddCell(new Cell().Add(new Paragraph(encounterDetails.Skin ?? "-")));

                nestedTable18.AddCell(new Cell().Add(new Paragraph("Neuro:").SetBold()).SetWidth(100));
                nestedTable18.AddCell(new Cell().Add(new Paragraph(encounterDetails.Neuro ?? "-")));

                nestedTable19.AddCell(new Cell().Add(new Paragraph("Other:").SetBold()).SetWidth(100));
                nestedTable19.AddCell(new Cell().Add(new Paragraph(encounterDetails.Other ?? "-")));

                nestedTable20.AddCell(new Cell().Add(new Paragraph("Diagnosis:").SetBold()).SetWidth(100));
                nestedTable20.AddCell(new Cell().Add(new Paragraph(encounterDetails.Diagnosis ?? "-")));

                nestedTable21.AddCell(new Cell().Add(new Paragraph("Treatment:").SetBold()).SetWidth(100));
                nestedTable21.AddCell(new Cell().Add(new Paragraph(encounterDetails.TreatmentPlan ?? "-")));

                nestedTable22.AddCell(new Cell().Add(new Paragraph("Dispensed:").SetBold()).SetWidth(100));
                nestedTable22.AddCell(new Cell().Add(new Paragraph(encounterDetails.MedicationsDispensed ?? "-")));

                nestedTable23.AddCell(new Cell().Add(new Paragraph("Procedures:").SetBold()).SetWidth(100));
                nestedTable23.AddCell(new Cell().Add(new Paragraph(encounterDetails.Procedures ?? "-")));

                nestedTable24.AddCell(new Cell().Add(new Paragraph("Followup:").SetBold()).SetWidth(100));
                nestedTable24.AddCell(new Cell().Add(new Paragraph(encounterDetails.Followup?? "-")));

                mainTable.AddCell(nestedTable1);
                mainTable.AddCell(nestedTable2);
                mainTable.AddCell(nestedTable3);
                mainTable.AddCell(nestedTable4);
                mainTable.AddCell(nestedTable5);
                mainTable.AddCell(nestedTable6);
                mainTable.AddCell(nestedTable7);
                mainTable.AddCell(nestedTable8);
                mainTable.AddCell(nestedTable9);
                mainTable.AddCell(nestedTable10);
                mainTable.AddCell(nestedTable11);
                mainTable.AddCell(nestedTable12);
                mainTable.AddCell(nestedTable13);
                mainTable.AddCell(nestedTable14);
                mainTable.AddCell(nestedTable15);
                mainTable.AddCell(nestedTable16);
                mainTable.AddCell(nestedTable17);
                mainTable.AddCell(nestedTable18);
                mainTable.AddCell(nestedTable19);
                mainTable.AddCell(nestedTable20);
                mainTable.AddCell(nestedTable21);
                mainTable.AddCell(nestedTable22);
                mainTable.AddCell(nestedTable23);
                mainTable.AddCell(nestedTable24);
                document.Add(mainTable);

                document.Close();

                return memoryStream.ToArray();
            }

          
        }

        public async Task<bool> Finalize(int Requestid)
        {
            Request request = _requestRepo.GetRequest(Requestid);
            request.IsMobile = new System.Collections.BitArray(1, true);
            await _requestRepo.UpdateTable(request);
            return true;
        }
        public ConcludeCareVM GetConcludeCareFile(int RequestId)
        {
            List<RequestWiseFile> requestWiseFile = _requestFileRepo.GetAllFiles(RequestId);
            ConcludeCareVM concludeCareVM = new ConcludeCareVM();
            concludeCareVM.WiseFiles = requestWiseFile;
            concludeCareVM.reqid = RequestId;
            return concludeCareVM;
        }

        public async Task<bool> AddFileData(ConcludeCareVM concludeCareVM)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
            FileInfo fileInfo = new FileInfo(concludeCareVM.file.FileName);
            string fileName = concludeCareVM.file.FileName;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                concludeCareVM.file.CopyTo(stream);
            }

            RequestWiseFile requestWise = new RequestWiseFile();
            requestWise.RequestId = concludeCareVM.reqid;
            requestWise.FileName = fileName;
            requestWise.CreatedDate = DateTime.Now;
            await _requestFileRepo.AddData(requestWise);
            return true;
        }

        public async Task<bool> Conclude(ConcludeCareVM concludeCareVM)
        {
            RequestNote requestNote = _requestNoteRepo.GetNoteData(concludeCareVM.reqid);
            if(requestNote != null) 
            {
                requestNote.PhysicianNotes = concludeCareVM.ProviderNotes;
                requestNote.ModifiedDate = DateTime.Now;
                await _requestNoteRepo.UpdateTable(requestNote);

               
            }
            else
            {
                RequestNote requestNote1 = new();
                requestNote1.RequestId = concludeCareVM.reqid;
                requestNote1.PhysicianNotes = concludeCareVM.ProviderNotes;
                requestNote1.CreatedDate = DateTime.Now;
                await _requestNoteRepo.AddTable(requestNote1);
            }

            Request request = _requestRepo.GetRequest(concludeCareVM.reqid);
            request.Status = 8;
            await _requestRepo.UpdateTable(request);

            return true;
        }

        public async Task<bool> ToConclude(int Requestid)
        {
            Request request = _requestRepo.GetRequest(Requestid);
            request.Status = 6;
            await _requestRepo.UpdateTable(request);
            return true;
        }

    }
}
