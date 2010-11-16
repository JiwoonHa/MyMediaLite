// Copyright (C) 2010 Zeno Gantner
//
// This file is part of MyMediaLite.
//
// MyMediaLite is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// MyMediaLite is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with MyMediaLite.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MyMediaLite.correlation;
using MyMediaLite.data;
using MyMediaLite.data_type;


namespace MyMediaLite.rating_predictor
{
	/// <summary>kNN recommender engine based on user attributes</summary>
	/// <remarks>
	/// This engine does NOT support online updates.
	/// </remarks>
	public class UserAttributeKNN : UserKNN, IUserAttributeAwareRecommender
	{
		/// <inheritdoc/>
		public SparseBooleanMatrix UserAttributes
		{
			get { return this.user_attributes; }
			set
			{
				this.user_attributes = value;
				this.NumUserAttributes = user_attributes.NumberOfColumns;
				this.MaxUserID = Math.Max(MaxUserID, user_attributes.NumberOfRows - 1);
			}
		}
		private SparseBooleanMatrix user_attributes;

		/// <inheritdoc/>
		public int NumUserAttributes { get; set; }

        /// <inheritdoc/>
        public override void Train()
        {
			base.Train();
			this.correlation = Cosine.Create(user_attributes);
        }

        /// <inheritdoc/>
		public override string ToString()
		{
			return string.Format("user-attribute-kNN k={0} reg_u={1} reg_i={2}",
			                     K == uint.MaxValue ? "inf" : K.ToString(), RegU, RegI);
		}
	}

}

