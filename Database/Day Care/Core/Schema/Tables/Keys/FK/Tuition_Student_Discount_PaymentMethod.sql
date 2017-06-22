ALTER TABLE dbo.Tuition
	ADD CONSTRAINT FK_Tuition_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	ALTER TABLE dbo.Tuition
	ADD CONSTRAINT FK_Tuition_Discount FOREIGN KEY 
	(
		DiscountId
	) 
	REFERENCES Discount
	(
		DiscountId
	)
GO
ALTER TABLE dbo.Tuition
	ADD CONSTRAINT FK_Tuition_PaymentMethod FOREIGN KEY 
	(
		PaymentMethodId
	) 
	REFERENCES PaymentMethod
	(
		PaymentMethodId
	)
GO
	
	
