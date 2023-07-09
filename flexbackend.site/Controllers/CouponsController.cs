using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFModels.EFModels;

namespace flexbackend.site.Controllers
{
    public class CouponsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Coupons
        public ActionResult Index()
        {
            var coupons = db.Coupons.Include(c => c.CouponCategory).Include(c => c.ProjectTag);
            return View(coupons.ToList());
        }
        [HttpPost]
        public ActionResult GetDatas(bool searchExpired = false, string searchDiscountName = null)
        {
            var coupons = db.Coupons.Include(c => c.CouponCategory).Include(c => c.ProjectTag);

            if (!searchExpired)
            {
                DateTime today = DateTime.Today;
                coupons = coupons.Where(c => c.EndDate > today || c.EndDate == null);
            }

            if (!string.IsNullOrEmpty(searchDiscountName))
            {
                coupons = coupons.Where(c => c.CouponName.Contains(searchDiscountName));
            }

            var data = coupons.Select(c => new
            {
                CouponCategoryId = c.fk_CouponCategoryId,
                CouponId = c.CouponId,
                CouponCategoryName = c.CouponCategory.CouponCategoryName,
                CouponName = c.CouponName,
                StartDate = c.StartDate,
                EndType = c.EndType,
                EndDays = c.EndDays,
                EndDate = c.EndDate,
                MinimumPurchaseAmount = c.MinimumPurchaseAmount,
                PersonMaxUsage = c.PersonMaxUsage,
                DiscountType = c.DiscountType,
                DiscountValue = c.DiscountValue,
                // 其他屬性...
            }).ToList();

            return Json(data);
        }
        [HttpPost]
        public ActionResult GetDataById(int couponId)
        {
            var coupon = db.Coupons
                            .Where(c => c.CouponId == couponId)
                            .Include(c => c.CouponCategory)
                            .Include(c => c.ProjectTag)
                            .Select(c => new
                            {
                                CouponCategoryId = c.fk_CouponCategoryId,
                                CouponCategoryName = c.CouponCategory.CouponCategoryName,
                                CouponName = c.CouponName,
                                StartDate = c.StartDate,
                                EndType = c.EndType,
                                EndDays = c.EndDays,
                                EndDate = c.EndDate,
                                MinimumPurchaseAmount = c.MinimumPurchaseAmount,
                                PersonMaxUsage = c.PersonMaxUsage,
                                DiscountType = c.DiscountType,
                                DiscountValue = c.DiscountValue,
                                RequirementType = c.RequirementType,
                                Requirement = c.Requirement,
                                RequiredProjectTagID = c.fk_RequiredProjectTagID,
                                ProjectTagName = c.ProjectTag.ProjectTagName
                            })
                            .FirstOrDefault();

            return Json(coupon);
        }

        // GET: Coupons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // GET: Coupons/Create
        public ActionResult Create()
        {
            ViewBag.fk_CouponCategoryId = new SelectList(db.CouponCategories.Where(c => c.CouponCategoryId != 5), "CouponCategoryId", "CouponCategoryName");
            ViewBag.fk_RequiredProjectTagID = new SelectList(db.ProjectTags.Where(p => p.Status), "ProjectTagId", "ProjectTagName");
            List<SelectListItem> discountType = new List<SelectListItem>
            {
                new SelectListItem { Value = "0" , Text = "金額" },
                new SelectListItem { Value = "1" , Text = "百分比" },
                new SelectListItem { Value = "2" , Text = "免運費" },
            };

            List<SelectListItem> conditionType = new List<SelectListItem>
            {
                new SelectListItem { Value = "0" , Text = "依照購買價格" },
                new SelectListItem { Value = "1" , Text = "依照商品件數" },
            };
            ViewBag.ConditionType = new SelectList(conditionType, "Value", "Text"); ;
            ViewBag.DiscountType = new SelectList(discountType, "Value", "Text", 0);
            return View();
        }

        // POST: Coupons/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CouponId,fk_CouponCategoryId,CouponName,CouponCode,MinimumPurchaseAmount,DiscountType,DiscountValue,StartDate,EndType,EndDays,EndDate,PersonMaxUsage,RequirementType,Requirement,fk_RequiredProjectTagID")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                // 模型驗證通過，執行相應的操作
                db.Coupons.Add(coupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // 模型驗證失敗，繼續顯示 Create 視圖並傳遞驗證錯誤訊息
            ViewBag.fk_CouponCategoryId = new SelectList(db.CouponCategories, "CouponCategoryId", "CouponCategoryName", coupon.fk_CouponCategoryId);
            ViewBag.fk_RequiredProjectTagID = new SelectList(db.ProjectTags.Where(p => p.Status), "ProjectTagId", "ProjectTagName", coupon.fk_RequiredProjectTagID);

            // 重新設置 ViewBag.DiscountType
            List<SelectListItem> discountType = new List<SelectListItem>
            {
                new SelectListItem { Value = "0" , Text = "金額" },
                new SelectListItem { Value = "1" , Text = "百分比" },
                new SelectListItem { Value = "2" , Text = "免運費" },
            };

                    // 重新設置 ViewBag.RequirementType
            List<SelectListItem> conditionType = new List<SelectListItem>
            {
                new SelectListItem { Value = "0" , Text = "依照購買價格" },
                new SelectListItem { Value = "1" , Text = "依照商品件數" },
            };
            ViewBag.DiscountType = new SelectList(discountType, "Value", "Text", coupon.DiscountType);
            ViewBag.RequirementType = new SelectList(conditionType, "Value", "Text", coupon.RequirementType);

            // 取得驗證錯誤訊息
            IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors);
            // 將錯誤訊息傳遞給 View
            ViewBag.ValidationErrors = errors;

            return View(coupon);
        }

        // GET: Coupons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupons.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }

            ViewBag.fk_CouponCategoryId = new SelectList(db.CouponCategories.Where(c => c.CouponCategoryId != 5), "CouponCategoryId", "CouponCategoryName", coupon.fk_CouponCategoryId);
            ViewBag.fk_RequiredProjectTagID = new SelectList(db.ProjectTags.Where(p => p.Status), "ProjectTagId", "ProjectTagName", coupon.fk_RequiredProjectTagID);
            List<SelectListItem> discountType = new List<SelectListItem>
            {
                new SelectListItem { Value = "0" , Text = "金額" },
                new SelectListItem { Value = "1" , Text = "百分比" },
                new SelectListItem { Value = "2" , Text = "免運費" },
            };

            List<SelectListItem> conditionType = new List<SelectListItem>
            {
                new SelectListItem { Value = "0" , Text = "依照購買價格" },
                new SelectListItem { Value = "1" , Text = "依照商品件數" },
            };
            ViewBag.DiscountType = new SelectList(discountType, "Value", "Text", coupon.DiscountType);
            ViewBag.RequirementType = new SelectList(conditionType, "Value", "Text", coupon.RequirementType);
            return View(coupon);
        }

        // POST: Coupons/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CouponId,fk_CouponCategoryId,CouponName,CouponCode,MinimumPurchaseAmount,DiscountType,DiscountValue,StartDate,EndType,EndDays,EndDate,PersonMaxUsage,RequirementType,Requirement,fk_RequiredProjectTagID")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coupon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_CouponCategoryId = new SelectList(db.CouponCategories, "CouponCategoryId", "CouponCategoryName", coupon.fk_CouponCategoryId);
            ViewBag.fk_RequiredProjectTagID = new SelectList(db.ProjectTags, "ProjectTagId", "ProjectTagName", coupon.fk_RequiredProjectTagID);
            return View(coupon);
        }

        // GET: Coupons/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Coupon coupon = db.Coupons.Find(id);
        //    if (coupon == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(coupon);
        //}

        // POST: Coupons/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Coupon coupon = db.Coupons.Find(id);
            db.Coupons.Remove(coupon);
            db.SaveChanges();
            return Content("Resource deleted successfully.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
