using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentarlCars.Tests.Validators
{
    public class ValidateUserTests
    {
        [Fact]
        public void When_name_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The name must be filled and can't be null.");
        }

        [Fact]
        public void When_name_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The name must be filled and can't be null.");
        }

        [Fact]
        public void When_phone_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Phone must be filled and can't be null.");
        }

        [Fact]
        public void When_phone_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Phone must be filled and can't be null.");
        }

        [Fact]
        public void When_identitydocument_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Identity Document must be filled and can't be null.");
        }

        [Fact]
        public void When_identitydocumento_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Identity Document must be filled and can't be null.");
        }

        [Fact]
        public void When_birthday_year_is_greater_than_current_year_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "2014-2541-7463",
                Birthday = DateTime.Now.AddYears(1)
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The year can't be greater or equal the actual year.");
        }

        [Fact]
        public void When_nationality_id_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 0 }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Nationality Country Id must be filled and can't be null.");
        }

        [Fact]
        public void When_gender_is_null_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Gender must be filled and can't be null.");
        }

        [Fact]
        public void When_gender_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Gender must be filled and can't be null.");
        }

        [Fact]
        public void When_CNH_is_null_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = "Female",
                CNH = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The CNH must be filled and can't be null.");
        }

        [Fact]
        public void When_CNH_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = "Female",
                CNH = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The CNH must be filled and can't be null.");
        }

        [Fact]
        public void When_countryCNH_id_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new() { Id = 0 }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The CNH Country Id must be filled and can't be null.");
        }

        [Fact]
        public void When_dateCNH_year_is_greater_than_current_year_should_throw_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = DateTime.Now.AddYears(1),
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Date of the CNH must be less than the current year and greater than the year of user's birthday plus 18.");
        }

        [Fact]
        public void When_dateCNH_is_less_than_birthday_plus18years_should_throw_new_exception()
        {

            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
            };
            user.DateCNH = user.Birthday.AddYears(17);

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Date of the CNH must be less than the current year and greater than the year of user's birthday plus 18.");
        }

        [Fact]
        public void When_password_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Password must be filled and can't be null.");
        }

        [Fact]
        public void When_password_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Password must be filled and can't be null.");
        }

        [Fact]
        public void When_street_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = null
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Street must be filled and can't be null.");
        }

        [Fact]
        public void When_street_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = ""
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Street must be filled and can't be null.");
        }

        [Fact]
        public void When_number_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = null
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Number must be filled and can't be null.");
        }

        [Fact]
        public void When_number_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = ""
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Number must be filled and can't be null.");
        }

        [Fact]
        public void When_neighborhood_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = null
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Neighborhood must be filled and can't be null.");
        }

        [Fact]
        public void When_neighborhood_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = ""
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Neighborhood must be filled and can't be null.");
        }

        [Fact]
        public void When_city_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = null
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The City must be filled and can't be null.");
        }

        [Fact]
        public void When_city_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = ""
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The City must be filled and can't be null.");
        }

        [Fact]
        public void When_state_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = null
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The State must be filled and can't be null.");
        }

        [Fact]
        public void When_state_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = ""
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The State must be filled and can't be null.");
        }

        [Fact]
        public void When_postcode_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = null
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Post Code must be filled and can't be null.");
        }

        [Fact]
        public void When_postcode_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = ""
                }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Post Code must be filled and can't be null.");
        }

        [Fact]
        public void When_country_id_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = "K5 I857",
                    Country = new()
                    {
                        Id = 0
                    }
                }
            };

            ValidateUser validatorUser = new();
            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Country Id must be filled and can't be null.");
        }

        [Fact]
        public void When_email_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = "K5 I857",
                    Country = new()
                    {
                        Id = 1
                    }
                },
                Email = null
            };

            ValidateUser validatorUser = new();
            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Email must be filled and can't be null.");
        }

        [Fact]
        public void When_email_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = "K5 I857",
                    Country = new()
                    {
                        Id = 1
                    }
                },
                Email = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Email must be filled and can't be null.");
        }

        [Fact]
        public void When_email_doesnot_have_atsign_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = "K5 I857",
                    Country = new()
                    {
                        Id = 1
                    }
                },
                Email = "paulafoxgmail.com"
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Email must have a @");
        }

        [Fact]
        public void When_email_doesnot_have_characters_after_atsign_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = "K5 I857",
                    Country = new()
                    {
                        Id = 1
                    }
                },
                Email = "paulafox@"
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Email must have characters after the @");
        }

        [Fact]
        public void When_email_doesnot_have_dotch_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = "K5 I857",
                    Country = new()
                    {
                        Id = 1
                    }
                },
                Email = "paulafox@gmailcom"
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Email must have a .");
        }

        [Fact]
        public void When_user_doesnot_have_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = new DateTime(2005, 01, 01),
                Password = "O8)^K%6-}h!Fm",
                Address = new()
                {
                    Street = "Love Street",
                    Number = "27",
                    Neighborhood = "Trees",
                    City = "London",
                    State = "London",
                    PostalCode = "K5 I857",
                    Country = new()
                    {
                        Id = 1
                    }
                },
                Email = "paulafox@gmail.com"
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().NotThrow<Exception>();
        }
    }
}
