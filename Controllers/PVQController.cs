using MVCTEST.Data;
using MVCTEST.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCTEST.Controllers
{
    public class PVQController : Controller
    {
        pvqviewmodel _pvqviewmodel = new pvqviewmodel();

        public ActionResult PvqQuestions(int? id)
        {
            try
            {

                if (id == null || id == 0)
                {
                    TempData["message"] = "User ID Cannot be blank or Zero";
                    return RedirectToAction("getUserId", "PVQ");
                }
                else
                {
                    Session["id"] = id;
                    TempData["count1"] = 0;

                    List<Pvqquestionsmodel> questions = FetchDate(id);
                    List<pvqviewmodel> _viewModelList = new List<pvqviewmodel>();
                    questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                    return View(_viewModelList);
                }
            }
            catch (System.Exception)
            {
                Session.Abandon();
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PvqQuestions(List<pvqviewmodel> answers)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());
                List<Pvqquestionsmodel> questions = FetchDate(id);
                List<pvqviewmodel> _viewModelList = new List<pvqviewmodel>();
                for (int i = 0; i < questions.Count; i++)
                {
                    if (answers[i].Useranswer != null && answers[i + 1].Useranswer != null && answers[i + 2].Useranswer != null)
                    {
                        if (questions[i].Answer.Trim() == answers[i].Useranswer.Trim() && questions[i + 1].Answer.Trim() == answers[i + 1].Useranswer.Trim() && questions[i + 2].Answer.Trim() == answers[i + 2].Useranswer.Trim())
                        {
                            Session["pass"] = "A";
                            return RedirectToAction("PasswordReset");
                        }
                        else if (questions[i].Answer.Trim() == answers[i].Useranswer.Trim() && questions[i + 1].Answer.Trim() == answers[i + 1].Useranswer.Trim() && questions[i + 2].Answer.Trim() != answers[i + 2].Useranswer.Trim())
                        {
                            int count1 = Convert.ToInt32(TempData["count1"]);
                            count1 = count1 + 1;
                            TempData["count1"] = count1;
                            if (count1 != 3)
                            {
                                ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
                                questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                                ViewData[questions[i + 2].Q_ID.ToString()] = "Answer did not match. Please Verify";
                                return View(_viewModelList);
                            }
                            else
                            {
                                Session["pass"] = "B";
                                return RedirectToAction("PasswordReset");
                            }
                        }
                        else if (questions[i].Answer.Trim() == answers[i].Useranswer.Trim() && questions[i + 1].Answer.Trim() != answers[i + 1].Useranswer.Trim() && questions[i + 2].Answer.Trim() != answers[i + 2].Useranswer.Trim())
                        {
                            int count1 = Convert.ToInt32(TempData["count1"]);
                            count1 = count1 + 1;
                            TempData["count1"] = count1;
                            if (count1 != 3)
                            {
                                ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
                                questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                                ViewData[questions[i + 1].Q_ID.ToString()] = "Answer did not match. Please Verify";
                                ViewData[questions[i + 2].Q_ID.ToString()] = "Answer did not match. Please Verify";
                                return View(_viewModelList);
                            }
                            else
                            {
                                Session.Clear();
                                Session.Abandon();
                                return RedirectToAction("AccountLocked");
                            }
                        }
                        else if (questions[i].Answer.Trim() == answers[i].Useranswer.Trim() && questions[i + 1].Answer.Trim() != answers[i + 1].Useranswer.Trim() && questions[i + 2].Answer.Trim() == answers[i + 2].Useranswer.Trim())
                        {
                            int count1 = Convert.ToInt32(TempData["count1"]);
                            count1 = count1 + 1;
                            TempData["count1"] = count1;
                            if (count1 != 3)
                            {
                                ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
                                questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                                ViewData[questions[i + 1].Q_ID.ToString()] = "Answer did not match. Please Verify";
                                return View(_viewModelList);
                            }
                            else
                            {
                                Session["pass"] = "B";
                                return RedirectToAction("PasswordReset");
                            }
                        }
                        else if (questions[i].Answer.Trim() != answers[i].Useranswer.Trim() && questions[i + 1].Answer.Trim() != answers[i + 1].Useranswer.Trim() && questions[i + 2].Answer.Trim() == answers[i + 2].Useranswer.Trim())
                        {
                            int count1 = Convert.ToInt32(TempData["count1"]);
                            count1 = count1 + 1;
                            TempData["count1"] = count1;
                            if (count1 != 3)
                            {
                                ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
                                questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                                ViewData[questions[i].Q_ID.ToString()] = "Answer did not match. Please Verify";
                                ViewData[questions[i + 1].Q_ID.ToString()] = "Answer did not match. Please Verify";
                                return View(_viewModelList);
                            }
                            else
                            {
                                Session.Clear();
                                Session.Abandon();
                                return RedirectToAction("AccountLocked");
                            }
                        }
                        else if (questions[i].Answer.Trim() != answers[i].Useranswer.Trim() && questions[i + 1].Answer.Trim() == answers[i + 1].Useranswer.Trim() && questions[i + 2].Answer.Trim() == answers[i + 2].Useranswer.Trim())
                        {
                            int count1 = Convert.ToInt32(TempData["count1"]);
                            count1 = count1 + 1;
                            TempData["count1"] = count1;
                            if (count1 != 3)
                            {
                                ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
                                questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                                ViewData[questions[i].Q_ID.ToString()] = "Answer did not match. Please Verify";
                                return View(_viewModelList);
                            }
                            else
                            {
                                Session["pass"] = "B";
                                return RedirectToAction("PasswordReset");
                            }
                        }
                        else
                        {
                            int count1 = Convert.ToInt32(TempData["count1"]);
                            count1 = count1 + 1;
                            TempData["count1"] = count1;
                            if (count1 != 3)
                            {
                                ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
                                questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                                return View(_viewModelList);
                            }
                            else
                            {
                                Session.Clear();
                                Session.Abandon();
                                return RedirectToAction("AccountLocked");
                            }

                        }
                    }
                }


                questions.ForEach(x => _viewModelList.Add(new pvqviewmodel { Q_ID = x.Q_ID, Question = x.Question }));
                return View(_viewModelList);

            }
            catch (System.Exception)
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult AccountLocked()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {

                throw;
            }

        }



        [HttpGet]
        public ActionResult Error()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [HttpGet]
        public ActionResult getUserId()
        {
            try
            {
                User user = new User();
                return View(user);
            }
            catch (System.Exception)
            {
                Session.Abandon();
                return RedirectToAction("Error");
            }

        }
        [HttpPost]
        public ActionResult getUserId(User user)
        {
            try
            {
                if (checkUserExist(user) == true)
                {
                    //write code here to find whether user exist or not before proceeding
                    // return RedirectToAction("Question1", new { id = user.UserID });
                    return RedirectToAction("PvqQuestions", new { id = user.UserID });
                }
                else
                {
                    ModelState.AddModelError("UserID", "User does not exist");
                    return View(user);
                }

            }
            catch (System.Exception)
            {
                Session.Abandon();
                return RedirectToAction("Error");
            }

        }



        [HttpGet]
        public ActionResult PasswordReset()
        {
            try
            {
                if (Session["pass"].ToString() == "A" || Session["pass"].ToString() == "B")
                {
                    return View();
                }
                else if (Session["pass"].ToString()==null)   
                {
                    Session.Abandon();
                    Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session.Abandon();
                    return RedirectToAction("Error");
                }

            }
            catch (System.Exception)
            {
                Session.Abandon();
                return RedirectToAction("Error");
            }

        }

        [HttpPost]
        public ActionResult PasswordReset(User user)
        {
            try
            {
                if (user.Password == null && user.Password == null)
                {
                    return View(user);
                }
                else
                {
                    if (Session["pass"].ToString() == "B")
                    {
                        Session.Abandon();
                        Session.Clear();
                        return RedirectToAction("AccountLocked");
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        return RedirectToAction("Index", "Home");
                    }

                }

            }
            catch (System.Exception)
            {
                Session.Abandon();
                return RedirectToAction("Error");
            }

        }

        private bool checkUserExist(User user)
        {
            if (user.UserID == "1234")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Pvqquestionsmodel> FetchDate(int? id)
        {
            try
            {
                if (id != null || id != 0)
                {
                    PVQdata data = new PVQdata();
                    List<Pvqquestionsmodel> questions = data.GetQuestionsAndAnswersbyid(id);
                    return questions;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        //[HttpGet]
        //public ActionResult Question1(int? id)
        //{
        //    try
        //    {

        //        if (id == null || id == 0)
        //        {
        //            TempData["message"] = "User ID Cannot be blank or Zero";
        //            return RedirectToAction("getUserId", "PVQ");
        //        }
        //        else
        //        {
        //            Session["id"] = id;
        //            List<Pvqquestionsmodel> questions = FetchDate(id);
        //            Pvqquestionsmodel pvqquestionsmodel = questions[0];
        //            _pvqviewmodel.Q_ID = pvqquestionsmodel.Q_ID;
        //            _pvqviewmodel.Question = pvqquestionsmodel.Question;
        //            return View(_pvqviewmodel);
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        Session.Abandon();
        //        return RedirectToAction("Error");
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Question1(pvqviewmodel answers)
        //{
        //    try
        //    {
        //        int id = int.Parse(Session["id"].ToString());
        //        List<Pvqquestionsmodel> questions = FetchDate(id);
        //        Pvqquestionsmodel pvqquestionsmodel = questions[0];
        //        if (pvqquestionsmodel.Answer == answers.Useranswer)
        //        {
        //            Session["key"] = id + id * 100;
        //            return RedirectToAction("Question2", new { id = id });
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
        //            answers.Question = pvqquestionsmodel.Question;
        //            answers.Q_ID = pvqquestionsmodel.Q_ID;
        //            return View(answers);
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        Session.Abandon();
        //        return RedirectToAction("Error");
        //    }
        //}

        //[HttpGet]
        //public ActionResult Question2()
        //{
        //    try
        //    {
        //        int key = int.Parse(Session["key"].ToString());

        //        int? id = int.Parse(Session["id"].ToString());
        //        if (key == id + id * 100)
        //        {
        //            if ((id == null || id == 0))
        //            {
        //                return RedirectToAction("getUserId");
        //            }
        //            else
        //            {
        //                List<Pvqquestionsmodel> questions = FetchDate(id);
        //                Pvqquestionsmodel pvqquestionsmodel = questions[1];
        //                _pvqviewmodel.Q_ID = pvqquestionsmodel.Q_ID;
        //                _pvqviewmodel.Question = pvqquestionsmodel.Question;
        //                return View(_pvqviewmodel);
        //            }
        //        }
        //        else
        //        {
        //            Session.Abandon();
        //            return RedirectToAction("Error");
        //        }


        //    }
        //    catch (System.Exception)
        //    {
        //        Session.Abandon();
        //        return RedirectToAction("Error");
        //    }

        //}
        //[HttpPost]
        //public ActionResult Question2(pvqviewmodel answers)
        //{
        //    try
        //    {
        //        int? id = int.Parse(Session["id"].ToString());
        //        if (id == null || id == 0)
        //        {
        //            return RedirectToAction("getUserId");
        //        }
        //        else
        //        {
        //            List<Pvqquestionsmodel> questions = FetchDate(id);
        //            Pvqquestionsmodel pvqquestionsmodel = questions[1];
        //            if (pvqquestionsmodel.Answer == answers.Useranswer)
        //            {
        //                Session["key"] = id + id * 101;
        //                return RedirectToAction("Question3", new { id = id });
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");

        //                answers.Question = pvqquestionsmodel.Question;
        //                answers.Q_ID = pvqquestionsmodel.Q_ID;
        //                return View(answers);
        //            }
        //        }

        //    }
        //    catch (System.Exception)
        //    {
        //        Session.Abandon();
        //        return RedirectToAction("Error");
        //    }

        //}
        //[HttpGet]
        //public ActionResult Question3()
        //{
        //    try
        //    {
        //        int key = int.Parse(Session["key"].ToString());
        //        int? id = int.Parse(Session["id"].ToString());
        //        if (key == id + id * 101)
        //        {


        //            if (id == null || id == 0)
        //            {
        //                return RedirectToAction("getUserId");
        //            }
        //            else
        //            {
        //                List<Pvqquestionsmodel> questions = FetchDate(id);
        //                Pvqquestionsmodel pvqquestionsmodel = questions[2];
        //                _pvqviewmodel.Q_ID = pvqquestionsmodel.Q_ID;
        //                _pvqviewmodel.Question = pvqquestionsmodel.Question;
        //                return View(_pvqviewmodel);
        //            }
        //        }
        //        else
        //        {
        //            Session.Abandon();
        //            return RedirectToAction("Error");
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        Session.Abandon();
        //        return RedirectToAction("Error");
        //    }

        //}


        //[HttpPost]
        //public ActionResult Question3(pvqviewmodel answers)
        //{
        //    try
        //    {
        //        int id = int.Parse(Session["id"].ToString());
        //        List<Pvqquestionsmodel> questions = FetchDate(id);
        //        Pvqquestionsmodel pvqquestionsmodel = questions[2];
        //        if (pvqquestionsmodel.Answer == answers.Useranswer)
        //        {
        //            return RedirectToAction("PasswordReset");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("Useranswer", "Answer did not match. Please Verify");
        //            answers.Question = pvqquestionsmodel.Question;
        //            answers.Q_ID = pvqquestionsmodel.Q_ID;
        //            return View(answers);
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        Session.Abandon();
        //        return RedirectToAction("Error");
        //    }

        //}

    }
}
